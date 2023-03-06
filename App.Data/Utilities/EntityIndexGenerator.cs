using System.Reflection;
using App.Data.Attributes;
using App.Data.Extensions;
using App.Infrastructure.Options;
using App.Infrastructure.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace App.Data.Utilities;

public class EntityIndexGenerator : IEntityIndexGenerator
{
    private readonly IMongoDatabase _database;

    private readonly ILogger<EntityIndexGenerator> _logger;

    public EntityIndexGenerator(IOptions<DatabaseOptions> options, IMongoClient client, ILogger<EntityIndexGenerator> logger)
    {
        this._logger = logger;
        this._database = client.GetDatabase(options.Value.DatabaseName);
    }

    public ValueTask Generate(Assembly? assemblyToSearch = null)
    {
        var types = DiscoveryHelper.Discover<IEntity>(assemblyToSearch);
        return this.Generate(types);
    }

    public async ValueTask Generate(Type[] entityTypes)
    {
        foreach (var entityType in entityTypes)
        {
            var indexDefinitions = entityType.GetCustomAttributes<IndexDefinitionAttribute>().ToList();

            var searchIndexDefinition = entityType.GetCustomAttribute<SearchIndexDefinitionAttribute>();

            if (!indexDefinitions.Any() && searchIndexDefinition == null) continue;

            var collection = this._database.GetCollection(entityType);

            if (indexDefinitions.Any())
            {
                foreach (var indexDefinition in indexDefinitions)
                {
                    await _generateIndex(indexDefinition, entityType, collection);
                }
            }

            if (searchIndexDefinition != null)
            {
                await _generateSearchIndex(searchIndexDefinition, entityType, collection);
            }
        }
    }

    private async ValueTask _generateSearchIndex(SearchIndexDefinitionAttribute indexDefinition, Type entityType, IMongoCollection<IEntity> collection)
    {
        var existingIndexes = await (await collection.Indexes.ListAsync()).ToListAsync();

        var existingIndex =
            existingIndexes.FirstOrDefault(index => index.GetElement("name").Value.AsString == indexDefinition.Name);

        if (existingIndex == null)
        {
            var keys = new BsonDocumentIndexKeysDefinition<IEntity>(new BsonDocument());
            var options = new CreateIndexOptions();

            foreach (var property in entityType.GetProperties())
            {
                var attributes = property.GetCustomAttributes<IndexedPropertyAttribute>();

                if (attributes.FirstOrDefault(attr => attr.IndexName == indexDefinition.Name) is IndexedPropertyAttribute attr)
                {
                    keys.Document.Add(new BsonElement(property.Name, "text"));

                    if (attr.Weight != 1)
                    {
                        if (options.Weights == null)
                        {
                            options.Weights = new BsonDocument();
                        }

                        options.Weights.Add(new BsonElement(property.Name, attr.Weight));
                    }
                }
            }

            try
            {
                await collection.Indexes.CreateOneAsync(new CreateIndexModel<IEntity>(keys, options));
                this._logger.LogInformation("Created search index for {entity}", entityType.Name);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Could not create search index for {entity}", entityType.Name);
            }
        }
    }

    private async ValueTask _generateIndex(IndexDefinitionAttribute indexDefinition, Type entityType, IMongoCollection<IEntity> collection)
    {
        var existingIndexes = await (await collection.Indexes.ListAsync()).ToListAsync();

        var existingIndex =
            existingIndexes.FirstOrDefault(index => index.GetElement("name").Value.AsString == indexDefinition.Name);

        if (existingIndex == null)
        {
            var keys = new BsonDocumentIndexKeysDefinition<IEntity>(new BsonDocument());
            var options = new CreateIndexOptions();

            foreach (var property in entityType.GetProperties())
            {
                var attributes = property.GetCustomAttributes<IndexedPropertyAttribute>();

                if (attributes.FirstOrDefault(attr => attr.IndexName == indexDefinition.Name) is IndexedPropertyAttribute attr)
                {
                    if (attr.Hashed)
                    {
                        keys.Document.Add(new BsonElement(property.Name, "hashed"));
                        continue;
                    }

                    keys.Document.Add(new BsonElement(property.Name, (int)attr.Order));
                }
            }

            try
            {
                await collection.Indexes.CreateOneAsync(new CreateIndexModel<IEntity>(keys, options));
                this._logger.LogInformation(
                    keys.Document.Any(element => element.Value == "hashed")
                        ? "Created shard index for {entity}"
                        : "Created index for {entity}", entityType.Name);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Could not create index for {entity}", entityType.Name);
            }
        }
    }
}

public interface IEntityIndexGenerator
{
    ValueTask Generate(Assembly? assemblyToSearch = null);

    ValueTask Generate(Type[] entityTypes);
}