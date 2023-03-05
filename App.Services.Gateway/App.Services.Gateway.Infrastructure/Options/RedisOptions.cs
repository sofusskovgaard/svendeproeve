namespace App.Services.Gateway.Infrastructure.Options;

public class RedisOptions
{
    public string ConnectionString =>
        Environment.GetEnvironmentVariable("REDIS_URI") ?? "redis";
}