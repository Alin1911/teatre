namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class UserFile : BaseEntity
{
    public string Path { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    /// <summary>
    /// This property is used as a foreign to the user table in the database and as a correlation key for the ORM.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// This is a navigation property for the ORM to correlate this entity with the entity that it references via the foreign key.
    /// </summary>
    public User User { get; set; } = default!;
}
