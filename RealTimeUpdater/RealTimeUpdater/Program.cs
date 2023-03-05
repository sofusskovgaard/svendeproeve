using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using ProtoBuf.Grpc.Server;
using RealTimeUpdater.Infrastructure;
using RealTimeUpdater.Infrastructure.Hubs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddAutoMapper(Assembly.Load("RealTimeUpdater.Infrastructure"));

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq(Assembly.Load("RealTimeUpdater.Infrastructure"));

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var entityIndexGenerator = app.Services.GetRequiredService<IEntityIndexGenerator>();
await entityIndexGenerator.Generate(Assembly.Load("RealTimeUpdater.Data"));

app.UseResponseCompression();
app.MapGrpcService<ServiceNameGrpcService>();
app.MapCodeFirstGrpcReflectionService();
app.MapHub<ChatHub>("/chathub");

await app.RunAsync();

// it may not be best practice and quite frankly horrifying to see
// assemblies specified using their names, but there's a purpose.
// that purpose is because we use a template to create each service.
// this way we can iterate and create new services at a much higher speed.
// - sofusskovgaard