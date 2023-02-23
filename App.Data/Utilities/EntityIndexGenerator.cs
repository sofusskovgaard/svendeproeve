using System.Reflection;
using App.Data.Attributes;
using App.Data.Extensions;
using App.Infrastructure.Options;
using App.Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace App.Data.Utilities;

public class EntityIndexGenerator : IEntityIndexGenerator
{
    private readonly IMongoDatabase _database;

    public EntityIndexGenerator(IOptions<DatabaseOptions> options, IMongoClient client)
    {
        this._database = client.GetDatabase(options.Value.DatabaseName);
    }

    public Task Generate(Assembly? assemblyToSearch = null)
    {
        var types = DiscoveryHelper.Discover<IEntity>(assemblyToSearch);
        return this.Generate(types);
    }

    public async Task Generate(Type[] entityTypes)
    {
        foreach (var entityType in entityTypes)
        {
            var indexDefinitions = entityType.GetCustomAttributes<IndexDefinitionAttribute>();

            if (!indexDefinitions.Any()) continue;

            var collection = this._database.GetCollection(entityType);

            foreach (var indexDefinition in indexDefinitions)
            {
                var cursor = await collection.Indexes.ListAsync();

                var foundIndex = false;

                while (!foundIndex && await cursor.MoveNextAsync())
                    if (cursor.Current.Any(x => x.GetElement("name").Value.AsString.Equals(indexDefinition.Name)))
                        foundIndex = true;

                if (!foundIndex)
                {
                    var properties = entityType.GetProperties().Where(x =>
                        x.GetCustomAttribute<IndexedPropertyAttribute>() is IndexedPropertyAttribute attr &&
                        attr.IndexName.Equals(indexDefinition.Name));

                    var keys = new BsonDocumentIndexKeysDefinition<IEntity>(new { }.ToBsonDocument());

                    foreach (var propertyInfo in properties)
                    {
                        var propertyAttr = propertyInfo.GetCustomAttribute<IndexedPropertyAttribute>()!;
                        keys.Document.Add(new BsonElement(propertyInfo.Name, (int)propertyAttr.Order));
                    }

                    await collection.Indexes.CreateOneAsync(new CreateIndexModel<IEntity>(keys,
                        new CreateIndexOptions { Name = indexDefinition.Name, Unique = indexDefinition.IsUnique }));
                }
            }
        }
    }
}

public interface IEntityIndexGenerator
{
    Task Generate(Assembly? assemblyToSearch = null);

    Task Generate(Type[] entityTypes);
}