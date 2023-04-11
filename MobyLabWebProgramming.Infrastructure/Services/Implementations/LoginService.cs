using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MobyLabWebProgramming.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class LoginService : ILoginService
{
    private readonly JwtConfiguration _jwtConfiguration;

    /// <summary>
    /// Inject the required service configuration from the application.json or environment variables.
    /// </summary>
    public LoginService(IOptions<JwtConfiguration> jwtConfiguration) => _jwtConfiguration = jwtConfiguration.Value;

    public string GetToken(UserDTO user, DateTime issuedAt, TimeSpan expiresIn)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key); // Use the configured key as the encryption key to sing the JWT.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }), // Set the user ID as the "nameid" claim in the JWT.
            Claims = new Dictionary<string, object> // Add any other claims in the JWT, you can even add custom claims if you want.
            {
                { ClaimTypes.Name, user.Name },
                { ClaimTypes.Email, user.Email }
            },
            IssuedAt = issuedAt, // This sets the "iat" claim to indicate then the JWT was emitted.
            Expires = issuedAt.Add(expiresIn), // This sets the "exp" claim to indicate when the JWT expires and cannot be used.
            Issuer = _jwtConfiguration.Issuer, // This sets the "iss" claim to indicate the authority that issued the JWT.
            Audience = _jwtConfiguration.Audience, // This sets the "aud" claim to indicate to which client the JWT is intended to.
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Sign the JWT, it will set the algorithm in the JWT header to "HS256" for HMAC with SHA256.
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)); // Create the token.
    }
}
