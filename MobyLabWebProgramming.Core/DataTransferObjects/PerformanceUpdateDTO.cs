namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record PerformanceUpdateDTO(Guid Id, Guid? IdPiesa = default, Guid? HallId = default, DateTime? StartDate = default, DateTime? EndDate = default);
