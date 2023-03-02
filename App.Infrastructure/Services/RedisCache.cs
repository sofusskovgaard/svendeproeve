using System.Text;
using System.Text.Json;
using App.Infrastructure.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace App.Infrastructure.Services;

public class RedisCache : IRedisCache
{
    private readonly IConnectionMultiplexer _connection;

    private readonly IDatabaseAsync _db;

    public RedisCache(IOptions<RedisOptions> options)
    {
        _connection = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        _db = _connection.GetDatabase();
    }

    public RedisCache(IOptions<RedisOptions> options, int db)
    {
        _connection = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        _db = _connection.GetDatabase(db);
    }

    public async Task<string> GetAsync(string bucket, string key, TimeSpan? expiresIn = null)
    {
        var response = await _db.StringGetSetExpiryAsync(_key(bucket, key), expiresIn);
        return response.ToString();
    }

    public async Task<T?> GetAsync<T>(string bucket, string key, TimeSpan? expiresIn = null)
    {
        var response = await _db.StringGetSetExpiryAsync(_key(bucket, key), expiresIn);
        return await JsonSerializer.DeserializeAsync<T>(
            new MemoryStream(Encoding.UTF8.GetBytes(response.ToString())));
    }

    public async Task<string> GetOrSetAsync(string bucket, string key, Func<ValueTask<string>> func, TimeSpan? expiresIn = null, bool slide = false)
    {
        var result = await GetAsync(bucket, key, slide ? expiresIn : null);

        if (!string.IsNullOrEmpty(result)) return result;

        result = await func.Invoke();

        await SetAsync(bucket, key, result, expiresIn);

        return result;
    }

    public async Task<T> GetOrSetAsync<T>(string bucket, string key, Func<ValueTask<T>> func, TimeSpan? expiresIn = null, bool slide = false)
    {
        var result = await GetAsync<T>(bucket, key, slide ? expiresIn : null);

        if (result != null) return result;

        result = await func.Invoke();

        await SetAsync(bucket, key, result, expiresIn);

        return result;
    }

    public async Task<bool> SetAsync(string bucket, string key, string value, TimeSpan? expiresIn = null)
    {
        return await _db.StringSetAsync(_key(bucket, key), new RedisValue(value), expiresIn);
    }

    public async Task<bool> SetAsync<T>(string bucket, string key, T value, TimeSpan? expiresIn = null)
    {
        var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, value);
        return await _db.StringSetAsync(_key(bucket, key), new RedisValue(Encoding.UTF8.GetString(stream.ToArray())), expiresIn);
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
}