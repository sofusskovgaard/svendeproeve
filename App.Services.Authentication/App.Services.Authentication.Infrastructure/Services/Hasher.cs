using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace App.Services.Authentication.Infrastructure.Services;

public static class Hasher
{
    public static HasherResponse Hash(string value, bool useSalt = true)
    {
        if (useSalt)
        {
            var saltByteArray = _generateSalt();
            return new HasherResponse(Convert.ToBase64String(saltByteArray), Convert.ToBase64String(_generateHash(value, saltByteArray)));
        }
        return new HasherResponse(string.Empty, Convert.ToBase64String(_generateHash(value)));
    }
        
    public static bool VerifyValue(string value, string hash, string? salt = null)
    {
        var hashByteArray = Convert.FromBase64String(hash);
        var verificationHashByteArray = _generateHash(value, string.IsNullOrEmpty(salt) ? null : Convert.FromBase64String(salt));
        return hashByteArray.SequenceEqual(verificationHashByteArray);
    }

    private static byte[] _generateSalt()
    {
        return RandomNumberGenerator.GetBytes(128);
    }

    private static byte[] _generateHash(string value, byte[]? salt = null)
    {
        return salt switch
        {
            null => SHA512.HashData(Encoding.UTF8.GetBytes(value)),
            _ => KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA512, 300000, 512 / 8)
        };
    }
}

public record HasherResponse(string Salt, string Hash);