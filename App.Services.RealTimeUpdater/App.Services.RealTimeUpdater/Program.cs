using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using App.Services.RealTimeUpdater.Infrastructure;
using App.Services.RealTimeUpdater.Infrastructure.Hubs;
using ProtoBuf.Grpc.Server;
using System.Reflection;
using System.Text;
using App.Infrastructure.Options;
using App.Services.Gateway.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using App.Services.Authentication.Infrastructure.Grpc;
using RealTimeUpdater.Infrastructure.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddRabbitMq(Assembly.Load("App.Services.RealTimeUpdater.Infrastructure"));

builder.Services.AddGrpcServiceClient<IAuthenticationGrpcService>();

builder.Services.AddSignalR();

builder.Services.AddScoped<IMatchHub, MatchHub>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddSingleton<IIssuerSigningKeyCache, IssuerSigningKeyCache>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCustomJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = JwtOptions.Issuer,
            ValidAudience = JwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key)),
        };
    });

builder.Services.AddCors(options => options.AddDefaultPolicy(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add services to the container.
var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("/chathub");
app.MapHub<MatchHub>("/matchhub");

await app.RunAsync();

// it may not be best practice and quite frankly horrifying to see
// assemblies specified using their names, but there's a purpose.
// that purpose is because we use a template to create each service.
// this way we can iterate and create new services at a much higher speed.
// - sofusskovgaard