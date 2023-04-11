namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class PieceDTO
{
    public Guid Id { get; set; }
    public string Titlu { get; set; }
    public string Autor { get; set; }
    public string Descriere { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}