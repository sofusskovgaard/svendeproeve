using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace App.Infrastructure.Utilities;

public static class Hasher
{
    public static HasherResponse Hash(string value)
    {
        var saltByteArray = Hasher._generateSalt();
        var hashByteArray = Hasher._generateHash(value, saltByteArray);

        return new HasherResponse(Convert.ToBase64String(saltByteArray), Convert.ToBase64String(hashByteArray));
    }
        
    public static bool VerifyValue(string value, string hash, string salt)
    {
        var saltByteArray = Convert.FromBase64String(salt);
        var hashByteArray = Convert.FromBase64String(hash);

        var verificationHashByteArray = Hasher._generateHash(value, saltByteArray);

        return hashByteArray.SequenceEqual(verificationHashByteArray);
    }

    private static byte[] _generateSalt()
    {
        var salt = new byte[128];

        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(salt);

        return salt;
    }

    private static byte[] _generateHash(string value, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA512, 1000, 512 / 8);
    }
}

public record HasherResponse(string Salt, string Hash);