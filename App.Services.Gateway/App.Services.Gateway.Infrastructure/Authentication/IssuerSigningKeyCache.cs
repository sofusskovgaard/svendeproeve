using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

namespace App.Services.Gateway.Infrastructure.Authentication;

public class IssuerSigningKeyCache : IIssuerSigningKeyCache
{
    private readonly IAuthenticationGrpcService _authenticationGrpcService;

    private MemoryCache _cache { get; } = new(new MemoryCacheOptions());

    public IssuerSigningKeyCache(IAuthenticationGrpcService authenticationGrpcService)
    {
        this._authenticationGrpcService = authenticationGrpcService;
    }

    public async ValueTask<Tuple<string, ECDsa>> GetKey()
    {
        if (!_cache.TryGetValue("ECDSA", out ECDsa ecdsa) || !_cache.TryGetValue("KEY_ID", out string? kid))
        {
            var key = await this._authenticationGrpcService.PublicKey(new GetPublicKeyGrpcCommandMessage());

            ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            ecdsa.ImportSubjectPublicKeyInfo(new ReadOnlySpan<byte>(Convert.FromBase64String(key.PublicKey)), out _);

            kid = key.KeyId;

            _cache.Set("ECDSA", ecdsa);
            _cache.Set("KEY_ID", kid);
        }

        return new Tuple<string, ECDsa>(kid, ecdsa);
    }
}

public interface IIssuerSigningKeyCache
{
    ValueTask<Tuple<string, ECDsa>> GetKey();
}
