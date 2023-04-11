namespace MobyLabWebProgramming.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the browser origin urls to allow browser clients to access the server without CORS errors.
/// </summary>
public class CorsConfiguration
{
    public string[] Origins { get; set; } = default!;
}
