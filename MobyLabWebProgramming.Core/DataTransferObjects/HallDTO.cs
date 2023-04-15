namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class HallDTO
{
    public Guid Id { get; set; }
    public string Nume { get; set; }
    public int Capacity { get; set; }
    public decimal AdministrationCost { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
