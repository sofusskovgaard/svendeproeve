using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using App.Services.RealTimeUpdater.Infrastructure;
using App.Services.RealTimeUpdater.Infrastructure.Hubs;
using ProtoBuf.Grpc.Server;
using RealTimeUpdater.Infrastructure.Hubs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq(Assembly.Load("App.Services.RealTimeUpdater.Infrastructure"));
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddDefaultPolicy(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped<IMatchHub, MatchHub>();

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