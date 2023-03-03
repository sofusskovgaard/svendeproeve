using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace App.Services.Gateway.Infrastructure;

public static class CustomJwtBearerExtensions
{
    public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
        => builder.AddCustomJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtBearerOptions> configureOptions)
        => builder.AddCustomJwtBearer(authenticationScheme, displayName: null, configureOptions: configureOptions);

    public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, string? displayName, Action<JwtBearerOptions> configureOptions)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<JwtBearerOptions>, CustomJwtBearerConfigureOptions>());
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerPostConfigureOptions>());
        return builder.AddScheme<JwtBearerOptions, CustomJwtBearerHandler>(authenticationScheme, displayName, configureOptions);
    }
}