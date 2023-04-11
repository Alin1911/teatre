namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class RoleDTO
{
    public Guid IdActor { get; set; }
    public Guid IdPiesa { get; set; }
    public string TitluRol { get; set; } = default!;
}