namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record TransactionUpdateDTO(Guid Id, string? Status = default, Guid? UserId = default, decimal? TotalPrice = default);
