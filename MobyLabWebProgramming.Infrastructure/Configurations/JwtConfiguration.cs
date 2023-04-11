namespace MobyLabWebProgramming.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the Json Web Token for the application.
/// </summary>
public class JwtConfiguration
{
    /// <summary>
    /// Key is used as the encryption key for the JWT signature.
    /// </summary>
    public string Key { get; set; } = default!;
    /// <summary>
    /// The issuer is who issues the JWT, it should be a URL.
    /// </summary>
    public string Issuer { get; set; } = default!;
    /// <summary>
    /// The audience is who consumes the JWT, usually it is the client application, it should be a URL.
    /// </summary>
    public string Audience { get; set; } = default!;
}