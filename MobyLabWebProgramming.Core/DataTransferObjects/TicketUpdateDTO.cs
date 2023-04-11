namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record TicketUpdateDTO(Guid Id, Guid? PerformanceId = default, Guid? UserId = default, Guid? TransactionId = default, decimal? Pret = default);
