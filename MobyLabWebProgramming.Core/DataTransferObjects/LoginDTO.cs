namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This is a DTO that contains the login request information.
/// Note that it is a record, the class declaration also serves as a constructor, you mai use records if they have few properties.
/// </summary>
public record LoginDTO(string Email, string Password);
