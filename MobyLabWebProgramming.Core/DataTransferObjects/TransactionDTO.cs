namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class TransactionDTO
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public Guid ClientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
