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
                new Claim("id", payload.Id),
                new Claim(JwtRegisteredClaimNames.Sub, payload.Username),
                new Claim(JwtRegisteredClaimNames.Email, payload.Email),
            }),
            Expires = DateTime.UtcNow.AddSeconds(JwtOptions.TokenLifeTime),
            Issuer = JwtOptions.Issuer,
            Audience = JwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key)), SecurityAlgorithms.HmacSha512Signature)
        };

        if (payload.Roles?.Length > 0)
        {
            descriptor.Subject.AddClaims(payload.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public static string GenerateRefreshToken()
    {
        var rnd = RandomNumberGenerator.GetBytes(32);
        return Convert.ToHexString(rnd);
    }
}

public record JwtPayload(string Id, string Username, string Email, params string[]? Roles);