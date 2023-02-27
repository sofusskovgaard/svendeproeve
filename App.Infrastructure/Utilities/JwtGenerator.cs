using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using App.Infrastructure.Options;
using MassTransit.Caching.Internals;
using Microsoft.IdentityModel.Tokens;

namespace App.Infrastructure.Utilities;

public static class JwtGenerator
{
    public static string GenerateAccessToken(JwtPayload payload)
    {
        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", payload.Id),
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, payload.Username),
                new Claim(JwtRegisteredClaimNames.Email, payload.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = JwtOptions.Issuer,
            Audience = JwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key)), SecurityAlgorithms.HmacSha512Signature)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public static string GenerateRefreshToken(string id)
    {
        // 8 bytes may be a little overkill since there are 18.446.744.073.709.551.616 possibilities
        var rnd = RandomNumberGenerator.GetBytes(8);
        return Convert.ToHexString(rnd);
    }
}

public record JwtPayload(string Id, string Username, string Email);