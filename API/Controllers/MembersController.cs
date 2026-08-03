using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class MembersController : BaseApiController
{
    private readonly AppDbContext _context;

    public MembersController(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDTO>>> GetMembers()
    {
        var users = await _context.AppUsers.Select(x => x.ToUserDto()).ToListAsync();
        return users;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetMembers(Guid id)
    {
        var user = await _context.AppUsers.FindAsync(id);

        if (user is null) return NotFound();

        return user.ToUserDto();
    }
}