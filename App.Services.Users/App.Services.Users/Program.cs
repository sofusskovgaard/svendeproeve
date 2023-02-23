using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using ProtoBuf.Grpc.Server;
using App.Services.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddAutoMapper(typeof(UsersGrpcService));

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