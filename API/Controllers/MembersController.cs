using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MembersController : BaseApiController
{
    private readonly IMemberRepository _repository;

    public MembersController(IMemberRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
    {
        var users = await _repository.GetMembersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetMember(Guid id)
    {
        var user = await _repository.GetMemberByIdAsync(id);

        if (user is null) return NotFound();

        return user;
    }

    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotos(Guid id)
    {
        var user = await _repository.GetPhotosForMemberAsync(id);

        if (user is null) return NotFound();

        return Ok(user);
    }
}