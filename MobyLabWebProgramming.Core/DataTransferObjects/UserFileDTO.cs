namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to transfer information about a user file within the application and to client application.
/// </summary>
public class UserFileDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public UserDTO User { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
