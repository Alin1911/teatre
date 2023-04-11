namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record TransactionUpdateDTO(Guid Id, DateTime? Data = default, Guid? UserId = default, decimal? Total = default);
