using System.Reflection;
using App.Data.Extensions;
using App.Infrastructure.Extensions;
using App.Services;
using ProtoBuf.Grpc.Server;
using Template.Services.ServiceName.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddMongoDb();

builder.Services.AddScoped<IServiceNameGrpcService, ServiceNameGrpcService>();
builder.Services.AddRabbitMq<ServiceNameService>();

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddGrpcCommandHandlers(Assembly.GetAssembly(typeof(TenantService)));

var app = builder.Build();

app.MapGrpcService<TenantService>();
app.MapCodeFirstGrpcReflectionService();

app.Run();