

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class PerformanceDTO
{
    public Guid Id { get; set; }
    public Guid IdPiesa { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid HallId { get; set; }
    public decimal Price { get; set; }
    public int TicketMaxScan { get; set; }
    public DateTime CreatedAt { get; internal set; }
    public DateTime UpdatedAt { get; internal set; }
}