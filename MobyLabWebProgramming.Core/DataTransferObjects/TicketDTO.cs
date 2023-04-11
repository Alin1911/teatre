namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class TicketDTO
{
    public Guid Id { get; set; }
    public Guid PerformanceId { get; set; }
    public Guid ClientId { get; set; }
    public int NumarScanari { get; set; }
    public Guid TransactionId { get; set; }
    public string Code { get; set; } = default!;
}