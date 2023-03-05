using App.Data.Extensions;
using App.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public async Task<T> GetEntity<T>(ExpressionFilterDefinition<T>? filter = default, FindOptions<T>? options = default) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var cursor = await collection.FindAsync(filter, options);
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<T> GetEntity<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter, FindOptions<T>? options = null) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var cursor = await collection.FindAsync(filter(new FilterDefinitionBuilder<T>()), options);
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> ListEntitiesByIds<T>(IEnumerable<string> ids) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        return await collection.AsQueryable().Where(x => ids.Any(y => x.Id == y)).ToListAsync();
    }

    public async Task<IEnumerable<T>> ListEntities<T>(ExpressionFilterDefinition<T> filter, FindOptions<T>? options = default) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var query = await collection.FindAsync(filter ?? FilterDefinition<T>.Empty, options);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> ListEntities<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>>? filter = null, FindOptions<T>? options = null) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var query = await collection.FindAsync(filter?.Invoke(new FilterDefinitionBuilder<T>()) ?? FilterDefinition<T>.Empty, options);
        return await query.ToListAsync();
    }

    public async Task<T> SaveEntity<T>(T entity) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();

        await collection.InsertOneAsync(entity);

        return entity;
    }

    public async Task<IEnumerable<T>> SaveEntities<T>(IEnumerable<T> entities) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();

        await collection.InsertManyAsync(entities);

        return entities;
    }

    [Obsolete("Use the other update method")]
    public async Task<T> Update<T>(T entity) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        await collection.UpdateOneAsync(f => f.Id == entity.Id, new ObjectUpdateDefinition<T>(entity));

        return entity;
    }

    public async Task<bool> Update<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> definition, UpdateOptions? options = null) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();

        var _definition = definition(new UpdateDefinitionBuilder<T>());

        _definition = _definition.Set(entity => entity.UpdatedTs, DateTime.UtcNow);

        var result = await collection.UpdateOneAsync(filter(new FilterDefinitionBuilder<T>()), _definition, options);
        return result.IsAcknowledged && result.MatchedCount > 0 && result.ModifiedCount > 0;
    }

    public async Task<bool> Delete<T>(T entity) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var result = await collection.DeleteOneAsync(f => f.Id == entity.Id);
        return result.IsAcknowledged;
    }

    public async Task<bool> Delete<T>(ExpressionFilterDefinition<T>? filter = default) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var result = await collection.DeleteOneAsync(filter);
        return result.IsAcknowledged;
    }

    public async Task<bool> Delete<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter) where T : IEntity
    {
        var collection = this._db.GetCollection<T>();
        var result = await collection.DeleteOneAsync(filter(new FilterDefinitionBuilder<T>()));
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