namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class TicketDTO
{
    public Guid Id { get; set; }
    public Guid PerformanceId { get; set; }
    public Guid UserId { get; set; }
    public int NumarScanari { get; set; }
    public Guid TransactionId { get; set; }
    public string Code { get; set; } = default!;
    public DateTime CreatedAt { get; internal set; }
    public DateTime UpdatedAt { get; internal set; }
}