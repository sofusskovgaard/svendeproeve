using App.Data.Extensions;
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

app.MapGrpcService<ServiceNameGrpcService>();
app.MapCodeFirstGrpcReflectionService();

app.Run();