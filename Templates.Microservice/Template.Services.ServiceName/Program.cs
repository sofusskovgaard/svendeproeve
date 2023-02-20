using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using ProtoBuf.Grpc.Server;
using Template.Services.ServiceName.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq<ServiceNameGrpcService>();

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var entityIndexGenerator = app.Services.GetRequiredService<IEntityIndexGenerator>();
await entityIndexGenerator.Generate();

app.MapGrpcService<ServiceNameGrpcService>();
app.MapCodeFirstGrpcReflectionService();

await app.RunAsync();