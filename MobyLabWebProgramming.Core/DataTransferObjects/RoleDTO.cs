namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class RoleDTO
{
    public Guid IdActor { get; set; }
    public Guid IdPiesa { get; set; }
    public string TitluRol { get; set; } = default!;
    public DateTime CreatedAt { get; internal set; }
    public DateTime UpdatedAt { get; internal set; }
    public Guid Id { get; internal set; }
}