using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using App.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Services.Authentication.Infrastructure.Services;

public class JwtGeneratorService : IJwtGeneratorService
{
    private readonly IJwtKeyService _jwtKeyService;

    public JwtGeneratorService(IJwtKeyService jwtKeyService)
    {
        _jwtKeyService = jwtKeyService;
    }

    public async ValueTask<string> GenerateAccessToken(JwtPayload payload)
    {
        var ecdsa = await _jwtKeyService.GetKey();

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
            SigningCredentials = new SigningCredentials(new ECDsaSecurityKey(ecdsa), SecurityAlgorithms.EcdsaSha256)
        };

        if (payload.IsAdmin)
        {
            descriptor.Subject.AddClaim(new Claim("isAdmin", "true", ClaimValueTypes.Boolean));
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var rnd = RandomNumberGenerator.GetBytes(32);
        return Convert.ToHexString(rnd);
    }
}

public interface IJwtGeneratorService
{
    ValueTask<string> GenerateAccessToken(JwtPayload payload);

    string GenerateRefreshToken();
}

public record JwtPayload(string Id, string Username, string Email, bool IsAdmin = false);