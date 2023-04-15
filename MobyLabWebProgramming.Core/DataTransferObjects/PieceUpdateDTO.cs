namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record PieceUpdateDTO(Guid Id, string? Titlu = default, string? Descriere = default, string? Autor = default);
