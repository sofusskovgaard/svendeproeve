﻿using App.Data.Utilities;
using App.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace App.Data.Extensions;

public static class DatabaseRegistrationExtensions
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        services.AddTransient<IEntityIndexGenerator, EntityIndexGenerator>();

        services.AddSingleton<IMongoClient, MongoClient>(provider =>
        {
            var options = provider.GetService<IOptions<DatabaseOptions>>();
            return new MongoClient(options.Value.ConnectionString);
        });
    }
}