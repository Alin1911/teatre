using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MobyLabWebProgramming.Infrastructure.Authorization;

public static class PasswordUtils
{
    private static readonly byte[] Salt =
    {
        0xAF, 0xA5, 0xB5, 0x46,
        0xD1, 0xA7, 0xB6, 0xB8,
        0xFD, 0xA1, 0xB2, 0x37,
        0xFA, 0xF1, 0x32, 0x46
    };

    /// <summary>
    /// This is a simple method to hash a password string.
    /// </summary>
    public static string HashPassword(string password) =>
        Convert.ToBase64String(KeyDerivation.Pbkdf2(password, Salt, KeyDerivationPrf.HMACSHA256, 1000, 256 / 8));
}
