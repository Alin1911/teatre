namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This is a DTO for transferring a file stream together with a name.
/// Note that it is a record, the class declaration also serves as a constructor, you mai use records if they have few properties.
/// </summary>
public record FileDTO(Stream Stream, string Name);
