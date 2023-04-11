namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record PerformanceUpdateDTO(Guid Id, Guid? IdPiesa = default, Guid? HallId = default, DateTime? Data = default, TimeSpan? Ora = default);
