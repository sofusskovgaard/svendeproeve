using App.Data.Extensions;
using App.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace App.Data.Services;

public class EntityDataService : IEntityDataService
{
    private readonly IMongoDatabase _db;

    public EntityDataService(IOptions<DatabaseOptions> options, IMongoClient client)
    {
        this._db = client.GetDatabase(options.Value.DatabaseName);
    }

    public async Task<T> GetEntity<T>(string id) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        return await (await collection.FindAsync(entity => entity.Id == id)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> ListEntitiesByIds<T>(IEnumerable<string> ids) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        return await collection.AsQueryable().Where(x => ids.Any(y => x.Id == y)).ToListAsync();
    }

    public async Task<IEnumerable<T>> ListEntities<T>() where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        return await collection.AsQueryable().ToListAsync();
    }

    public async Task<IEnumerable<T>> SaveEntities<T>(IEnumerable<T> entities) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();

        await collection.InsertManyAsync(entities);

        return entities;
    }

    public async Task<T> Update<T>(T entity) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        await collection.UpdateOneAsync(f => f.Id == entity.Id, new ObjectUpdateDefinition<T>(entity));

        return entity;
    }

    // public Task<IEnumerable<T>> Update<T>(IEnumerable<T> entities) where T : IEntity
    // {
    //     var collection = this._db.GetCollection<T>();
    //
    //     collection.UpdateManyAsync()
    //     
    //     return entities.Select(entity => Update(entity));
    // }

    public async Task<bool> Delete<T>(T entity) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var result = await collection.DeleteOneAsync(f => f.Id == entity.Id);
        return result.IsAcknowledged;
    }

    public async Task<bool> Delete<T>(string id) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var result = await collection.DeleteOneAsync(f => f.Id == id);
        return result.IsAcknowledged;
    }

    public async Task<T> Create<T>(T entity) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        await collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> Create<T>(IEnumerable<T> entities) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        await collection.InsertManyAsync(entities);
        return entities;
    }
}