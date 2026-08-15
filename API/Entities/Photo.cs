using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Url { get; set; }

    public string? PublicId { get; set; }

    public Guid MemberId { get; set; }

    // Navigation Property
    [JsonIgnore]
    [ForeignKey(nameof(MemberId))]
    public Member Member { get; set; } = null!;
}