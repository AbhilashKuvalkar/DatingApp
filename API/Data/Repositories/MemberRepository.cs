using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Member?> GetMemberByIdAsync(Guid guid, CancellationToken ct = default)
    {
        return await _context.Members.FindAsync([guid], cancellationToken: ct);
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync(CancellationToken ct = default)
    {
        return await _context.Members.ToListAsync(cancellationToken: ct);
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(Guid guid, CancellationToken ct = default)
    {
        return await _context.Members
            .Where(w => w.Id == guid)
            .SelectMany(s => s.Photos)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<bool> SaveAllAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(cancellationToken: ct) > 0;
    }

    public void Update(Member member)
    {
        _context.Entry(member).State = EntityState.Modified;
    }
}