using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Photo> Photos { get; set; }
}

