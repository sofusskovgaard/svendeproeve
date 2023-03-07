using MongoDB.Driver;

namespace App.Data.Services;

public interface IEntityDataService
{
    Task<T> GetEntity<T>(string id) where T : IEntity;

    [Obsolete("Use the other get entity method")]
    Task<T> GetEntity<T>(ExpressionFilterDefinition<T> filter, FindOptions<T>? options = null) where T : IEntity;

    Task<T> GetEntity<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter, FindOptions<T>? options = null) where T : IEntity;

    Task<IEnumerable<T>> ListEntities<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>>? filter = null, FindOptions<T>? options = null) where T : IEntity;

    Task<T> SaveEntity<T>(T entity) where T : IEntity;

    Task<IEnumerable<T>> SaveEntities<T>(IEnumerable<T> entities) where T : IEntity;

    Task<bool> Update<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
        Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> definition, UpdateOptions? options = null)
        where T : IEntity;

    Task<bool> Delete<T>(Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter) where T : IEntity;

    Task<T> Create<T>(T entity) where T : IEntity;

    Task<IEnumerable<T>> Create<T>(IEnumerable<T> entities) where T : IEntity;
}