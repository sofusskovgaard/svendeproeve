using System.Security.Cryptography;
using App.Data.Services;
using App.Services.Authentication.Data.Entities;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using static MassTransit.ValidationResultExtensions;

namespace App.Services.Authentication.Infrastructure.Services;

public class JwtKeyService : IJwtKeyService
{
    private readonly IEntityDataService _entityDataService;

    public JwtKeyService(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    private MemoryCache _cache { get; } = new(new MemoryCacheOptions());

    private const string ID_KEY = "KEY_ID";
    private const string ECDSA_PRIVATE_KEY = "ECDSA_PRIVATE";
    private const string ECDSA_PUBLIC_KEY = "ECDSA_PUBLIC";

    public async ValueTask<Tuple<string, ECDsa>> GetKey()
    {

        if (_cache.TryGetValue(ECDSA_PUBLIC_KEY, out string? publicKey))
        {
            if (_cache.TryGetValue(ECDSA_PRIVATE_KEY, out string? privateKey))
            {
                if (_cache.TryGetValue(ID_KEY, out string? kid))
                {
                    var ecdsa = ECDsa.Create();

                    var publicKeyArr = Convert.FromBase64String(publicKey);
                    var privateKeyArr = Convert.FromBase64String(privateKey);

                    ecdsa.ImportSubjectPublicKeyInfo(publicKeyArr, out _);
                    ecdsa.ImportECPrivateKey(privateKeyArr, out _);

                    return new Tuple<string, ECDsa>(kid, ecdsa);
                }
            }
        }

        JwtKeyEntity entity;

        var entities = await _entityDataService.ListEntities(filter => filter.Eq(entity => entity.IsActive, true), new FindOptions<JwtKeyEntity>()
        {
            Sort = new SortDefinitionBuilder<JwtKeyEntity>().Descending(entity => entity.CreatedTs)
        });

        entity = entities.FirstOrDefault();

        if (entity == null)
        {
            var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            entity = new JwtKeyEntity
            {
                PrivateKey = Convert.ToBase64String(ecdsa.ExportECPrivateKey()),
                PublicKey = Convert.ToBase64String(ecdsa.ExportSubjectPublicKeyInfo())
            };

            await _entityDataService.SaveEntity(entity);
        }

        _cache.Set(ID_KEY, entity.Id);
        _cache.Set(ECDSA_PRIVATE_KEY, entity.PrivateKey);
        _cache.Set(ECDSA_PUBLIC_KEY, entity.PublicKey);

        return await GetKey();
    }
}

public interface IJwtKeyService
{
    ValueTask<Tuple<string, ECDsa>> GetKey();
}