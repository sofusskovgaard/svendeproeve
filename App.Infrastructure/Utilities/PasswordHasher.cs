using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace App.Infrastructure.Utilities;

public static class PasswordHasher
{
    public static PasswordHasherResponse Hash(string password)
    {
        var saltByteArray = PasswordHasher._generateSalt();
        var hashByteArray = PasswordHasher._generateHash(password, saltByteArray);

        return new PasswordHasherResponse {
            Salt = Convert.ToBase64String(saltByteArray), Hash = Convert.ToBase64String(hashByteArray)
        };
    }

    public static bool VerifyPassword(string password, string hash, string salt)
    {
        var saltByteArray = Convert.FromBase64String(salt);
        var hashByteArray = Convert.FromBase64String(hash);

        var verificationHashByteArray = PasswordHasher._generateHash(password, saltByteArray);

        return hashByteArray.SequenceEqual(verificationHashByteArray);
    }

    private static byte[] _generateSalt()
    {
        var salt = new byte[128];

        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(salt);

        return salt;
    }

    private static byte[] _generateHash(string password, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 1000, 512 / 8);
    }
}

public class PasswordHasherResponse : IPasswordHasherResponse
{
    public string Salt { get; set; }

    public string Hash { get; set; }
}

public interface IPasswordHasherResponse
{
    public string Salt { get; }

    public string Hash { get; }
}