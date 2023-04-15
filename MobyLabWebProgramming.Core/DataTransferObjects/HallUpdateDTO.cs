namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record HallUpdateDTO(Guid Id, string? Nume = default, int? AdministrationCost = default, int? Capacity = default);
