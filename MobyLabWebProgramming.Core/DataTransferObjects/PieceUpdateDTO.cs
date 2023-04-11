namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record PieceUpdateDTO(Guid Id, string? Titlu = default, string? Descriere = default, TimeSpan? Durata = default);
