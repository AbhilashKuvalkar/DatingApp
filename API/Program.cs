using System.Text;
using API.Data;
using API.Data.Repositories;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = builder.Configuration["TokenKey"] ??
            throw new KeyNotFoundException("Token key not found");

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
        };
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x =>
{
    x.AllowAnyHeader().AllowAnyMethod();
    x.WithOrigins(["http://localhost:4200", "https://localhost:4200"]);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (System.Exception exception)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, "An error occurred during migration");
}

app.Run();
