using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace App.Services.Gateway.Infrastructure.Authentication;

public static class CustomJwtBearerExtensions
{
    public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
        => CustomJwtBearerExtensions.AddCustomJwtBearer(builder, JwtBearerDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtBearerOptions> configureOptions)
        => CustomJwtBearerExtensions.AddCustomJwtBearer(builder, authenticationScheme, displayName: null, configureOptions: configureOptions);

    public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, string? displayName, Action<JwtBearerOptions> configureOptions)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<JwtBearerOptions>, CustomJwtBearerConfigureOptions>());
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerPostConfigureOptions>());
        return builder.AddScheme<JwtBearerOptions, CustomJwtBearerHandler>(authenticationScheme, displayName, configureOptions);
    }
}