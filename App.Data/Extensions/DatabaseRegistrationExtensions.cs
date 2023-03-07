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

/// <summary>
///     Extension methods used to add the required boilerplate to enable MongoDB access
/// </summary>
public static class DatabaseRegistrationExtensions
{
    /// <summary>
    ///     Add MongoDB to <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add MongoDB to</param>
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