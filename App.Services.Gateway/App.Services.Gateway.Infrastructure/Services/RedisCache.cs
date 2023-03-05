using System.Text;
using System.Text.Json;
using App.Services.Gateway.Infrastructure.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace App.Services.Gateway.Infrastructure.Services;

public class RedisCache : IRedisCache
{
    private readonly IConnectionMultiplexer _connection;

    private readonly IDatabaseAsync _db;

    public RedisCache(IOptions<RedisOptions> options)
    {
        this._connection = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        this._db = this._connection.GetDatabase();
    }

    public RedisCache(IOptions<RedisOptions> options, int db)
    {
        this._connection = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        this._db = this._connection.GetDatabase(db);
    }

    public async Task<string> GetAsync(string bucket, string key, TimeSpan? expiresIn = null)
    {
        var response = await this._db.StringGetSetExpiryAsync(this._key(bucket, key), expiresIn);
        return response.ToString();
    }

    public async Task<T?> GetAsync<T>(string bucket, string key, TimeSpan? expiresIn = null)
    {
        var response = await this._db.StringGetSetExpiryAsync(this._key(bucket, key), expiresIn);
        return await JsonSerializer.DeserializeAsync<T>(
            new MemoryStream(Encoding.UTF8.GetBytes((string)response.ToString())));
    }

    public async Task<string> GetOrSetAsync(string bucket, string key, Func<ValueTask<string>> func, TimeSpan? expiresIn = null, bool slide = false)
    {
        var result = await this.GetAsync(bucket, key, slide ? expiresIn : null);

        if (!string.IsNullOrEmpty(result)) return result;

        result = await func.Invoke();

        await this.SetAsync(bucket, key, result, expiresIn);

        return result;
    }

    public async Task<T> GetOrSetAsync<T>(string bucket, string key, Func<ValueTask<T>> func, TimeSpan? expiresIn = null, bool slide = false)
    {
        var result = await this.GetAsync<T>(bucket, key, slide ? expiresIn : null);

        if (result != null) return result;

        result = await func.Invoke();

        await this.SetAsync(bucket, key, result, expiresIn);

        return result;
    }

    public async Task<bool> SetAsync(string bucket, string key, string value, TimeSpan? expiresIn = null)
    {
        return await this._db.StringSetAsync(this._key(bucket, key), new RedisValue(value), expiresIn);
    }

    public async Task<bool> SetAsync<T>(string bucket, string key, T value, TimeSpan? expiresIn = null)
    {
        var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, value);
        return await this._db.StringSetAsync(this._key(bucket, key), new RedisValue(Encoding.UTF8.GetString(stream.ToArray())), expiresIn);
    }

    public Task<bool> AddSetList(string bucket, string key, string value)
    {
        return this._db.SetAddAsync(this._key(bucket, key), new RedisValue(value));
    }

    public async Task<bool> AddSetList<T>(string bucket, string key, T value)
    {
        var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, value);
        return await this._db.SetAddAsync(this._key(bucket, key), new RedisValue(Encoding.UTF8.GetString(stream.ToArray())));
    }

    public Task<bool> RemoveSetList(string bucket, string key, string value)
    {
        return this._db.SetRemoveAsync(this._key(bucket, key), new RedisValue(value));
    }

    public async Task<bool> RemoveSetList<T>(string bucket, string key, T value)
    {
        var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, value);
        return await this._db.SetRemoveAsync(this._key(bucket, key),
            new RedisValue(Encoding.UTF8.GetString(stream.ToArray())));
    }

    public async Task<List<string>> ListSetAsync(string bucket, string key)
    {
        var values = await this._db.SetMembersAsync(this._key(bucket, key));
        return values.Select(value => value.ToString()).ToList();
    }

    public async Task<List<T>> ListSetAsync<T>(string bucket, string key)
    {
        var values = await this._db.SetMembersAsync(this._key(bucket, key));
        return values.Select(value => JsonSerializer.Deserialize<T>(value.ToString())!).ToList();
    }

    private RedisKey _key(string bucket, string key)
    {
        return new RedisKey(string.Join('-', bucket.ToLower(), key.ToLower()));
    }
}

public interface IRedisCache
{
    Task<string> GetAsync(string bucket, string key, TimeSpan? expiresIn = null);

    Task<T?> GetAsync<T>(string bucket, string key, TimeSpan? expiresIn = null);

    Task<string> GetOrSetAsync(string bucket, string key, Func<ValueTask<string>> func, TimeSpan? expiresIn = null,
        bool slide = false);

    Task<T> GetOrSetAsync<T>(string bucket, string key, Func<ValueTask<T>> func, TimeSpan? expiresIn = null,
        bool slide = false);

    Task<bool> SetAsync(string bucket, string key, string value, TimeSpan? expiresIn = null);

    Task<bool> SetAsync<T>(string bucket, string key, T value, TimeSpan? expiresIn = null);

    Task<bool> AddSetList(string bucket, string key, string value);

    Task<bool> AddSetList<T>(string bucket, string key, T value);

    Task<bool> RemoveSetList(string bucket, string key, string value);

    Task<bool> RemoveSetList<T>(string bucket, string key, T value);

    Task<List<string>> ListSetAsync(string bucket, string key);

    Task<List<T>> ListSetAsync<T>(string bucket, string key);
}