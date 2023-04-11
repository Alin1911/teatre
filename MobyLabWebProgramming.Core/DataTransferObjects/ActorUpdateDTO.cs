
namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record ActorUpdateDTO(Guid Id, string? Nume = null, string? Prenume = null, DateTime? DataNastere = null, decimal? Salariu = null);
