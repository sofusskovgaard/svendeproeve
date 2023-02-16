namespace App.Infrastructure.Options;

public class DatabaseOptions
{
    public string ConnectionString =>
        Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://mongo:mongo@localhost:27017/";

    public string DatabaseName => Environment.GetEnvironmentVariable("MONGO_DB") ?? "app_dev";
}