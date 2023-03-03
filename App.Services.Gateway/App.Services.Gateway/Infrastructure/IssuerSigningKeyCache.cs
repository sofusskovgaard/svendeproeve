using Microsoft.Extensions.Caching.Memory;

namespace App.Services.Gateway.Infrastructure;

public class IssuerSigningKeyCache
{
    public MemoryCache Cache { get; } = new(new MemoryCacheOptions());
}