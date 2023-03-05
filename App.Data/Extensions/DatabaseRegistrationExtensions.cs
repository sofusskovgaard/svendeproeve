using App.Data.Services;
using App.Data.Utilities;
using App.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace App.Data.Extensions;

public static class DatabaseRegistrationExtensions
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));

        services.AddTransient<IEntityIndexGenerator, EntityIndexGenerator>();

        services.AddTransient<IEntityDataService, EntityDataService>();

        services.AddSingleton<IMongoClient, MongoClient>(provider =>
        {
            var options = provider.GetService<IOptions<DatabaseOptions>>();
            return new MongoClient(options.Value.ConnectionString);
        });
    }
}