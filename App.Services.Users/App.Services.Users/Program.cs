using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using ProtoBuf.Grpc.Server;
using App.Services.Users.Infrastructure;
using App.Services.Organizations.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq<UsersGrpcService>();

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var entityIndexGenerator = app.Services.GetRequiredService<IEntityIndexGenerator>();
await entityIndexGenerator.Generate();

app.MapGrpcService<UsersGrpcService>();
app.MapCodeFirstGrpcReflectionService();

await app.RunAsync();