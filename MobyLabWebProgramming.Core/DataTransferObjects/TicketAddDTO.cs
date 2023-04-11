
namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record TicketAddDTO(Guid PerformanceId, Guid UserId, Guid? TransactionId, decimal Pret);
