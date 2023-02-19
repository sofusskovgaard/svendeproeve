using App.Data.Extensions;
using App.Infrastructure.Extensions;
using ProtoBuf.Grpc.Server;
using App.Services.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq<UsersGrpcService>();

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<UsersGrpcService>();
app.MapCodeFirstGrpcReflectionService();

app.Run();