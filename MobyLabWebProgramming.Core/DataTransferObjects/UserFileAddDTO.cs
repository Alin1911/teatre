using Microsoft.AspNetCore.Http;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to add a user file, it contains a IFormFile that has the stream to the file in a form and an additional property.
/// </summary>
public class UserFileAddDTO
{
    public IFormFile File { get; set; } = default!;
    public string? Description { get; set; }
}
