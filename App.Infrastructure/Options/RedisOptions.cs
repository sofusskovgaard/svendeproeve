namespace App.Infrastructure.Options;

public class RedisOptions
{
    public string ConnectionString =>
        Environment.GetEnvironmentVariable("REDIS_URI") ?? "redis";
}