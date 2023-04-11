namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class TransactionAddDTO
{
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public Guid ClientId { get; set; }
}
