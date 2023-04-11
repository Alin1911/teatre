namespace MobyLabWebProgramming.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure where files can be stored, you can customize however you want. Generally, it is better to use a
/// third-party service to store files rather than managing them yourself.
/// </summary>
public class FileStorageConfiguration
{
    public string SavePath { get; set; } = default!;
}
