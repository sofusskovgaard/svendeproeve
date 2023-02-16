namespace App.Data.Services;

public interface IEntityDataService
{
    Task<T> GetEntity<T>(string id) where T : IEntity;

    Task<IEnumerable<T>> ListEntitiesByIds<T>(IEnumerable<string> ids) where T : IEntity;

    Task<IEnumerable<T>> ListEntities<T>() where T : IEntity;

    Task<IEnumerable<T>> SaveEntities<T>(IEnumerable<T> entities) where T : IEntity;

    Task<T> Update<T>(T entity) where T : IEntity;

    // Task<IEnumerable<T>> Update<T>(IEnumerable<T> entities) where T : IEntity;

    Task<bool> Delete<T>(T entity) where T : IEntity;

    Task<bool> Delete<T>(string id) where T : IEntity;

    Task<T> Create<T>(T entity) where T : IEntity;

    Task<IEnumerable<T>> Create<T>(IEnumerable<T> entities) where T : IEntity;
}