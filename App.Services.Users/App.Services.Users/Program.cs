using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using App.Services.Users.Data.Entities;
using ProtoBuf.Grpc.Server;
using App.Services.Users.Infrastructure;
using App.Services.Users.Infrastructure.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddAutoMapper(typeof(UserEntityMapper));

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq<UsersGrpcService>();

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var entityIndexGenerator = app.Services.GetRequiredService<IEntityIndexGenerator>();
await entityIndexGenerator.Generate(typeof(UserEntity).Assembly);

app.MapGrpcService<UsersGrpcService>();
app.MapCodeFirstGrpcReflectionService();

await app.RunAsync();