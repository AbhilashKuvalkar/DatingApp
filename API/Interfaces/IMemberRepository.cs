using API.Entities;

namespace API.Interfaces;

public interface IMemberRepository
{
    void Update(Member member);

    Task<bool> SaveAllAsync(CancellationToken ct = default);

    Task<IReadOnlyList<Member>> GetMembersAsync(CancellationToken ct = default);

    Task<Member?> GetMemberByIdAsync(Guid guid, CancellationToken ct = default);

    Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(Guid guid, CancellationToken ct = default);


}