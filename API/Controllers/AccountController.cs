using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(AppDbContext context, ITokenService tokenService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserAuthDTO>> Register(RegisterDTO dto, CancellationToken ct = default)
    {
        if (await IsEmailExists(dto.Email, ct)) return BadRequest("Email taken");

        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync(ct);

        return user.ToUserAuthDto(_tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserAuthDTO>> Login(LoginDTO dto, CancellationToken ct = default)
    {
        var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == dto.Email.ToLower(), ct);

        if (user is null) return Unauthorized("Invalid email address");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return user.ToUserAuthDto(_tokenService);
    }

    private async Task<bool> IsEmailExists(string email, CancellationToken ct)
    {
        return await _context.AppUsers
            .AnyAsync(x => x.Email.ToLower() == email.ToLower(), ct);
    }
}
