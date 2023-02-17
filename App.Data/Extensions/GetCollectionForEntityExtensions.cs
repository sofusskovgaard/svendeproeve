using System.Reflection;
using App.Data.Attributes;
using MongoDB.Driver;

namespace App.Data.Extensions;

public static class GetCollectionForEntityExtensions
{
    /// <summary>
    ///     Get collection using only the entity type. Make sure the <see cref="CollectionDefinitionAttribute" /> is defined on
    ///     the entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity you wish to get a collection for</typeparam>
    /// <param name="database">The database to get the collection from</param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static IMongoCollection<TEntity> GetCollection<TEntity>(this IMongoDatabase database,
        MongoCollectionSettings settings = null) where TEntity : IEntity
    {
        var attr = typeof(TEntity).GetCustomAttribute<CollectionDefinitionAttribute>();
        return database.GetCollection<TEntity>(attr.Name, settings);
    }

    public static IMongoCollection<IEntity> GetCollection(this IMongoDatabase database,
        Type type, MongoCollectionSettings settings = null)
    {
        var attr = type.GetCustomAttribute<CollectionDefinitionAttribute>();
        return database.GetCollection<IEntity>(attr.Name, settings);
    }

    //public static IMongoCollection GetCollection(this IMongoDatabase database, Type entityType, MongoCollectionSettings settings = null) where TEntity : IEntity
    //{
    //    var attr = (CollectionDefinitionAttribute)typeof(TEntity).GetCustomAttribute(typeof(CollectionDefinitionAttribute))!;
    //    return database.GetCollection<TEntity>(attr.Name, settings);
    //}
}