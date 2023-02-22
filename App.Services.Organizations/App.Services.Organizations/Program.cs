using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using App.Services.Organizations.Infrastructure;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq<OrganizationsGrpcService>();

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var entityIndexGenerator = app.Services.GetRequiredService<IEntityIndexGenerator>();
await entityIndexGenerator.Generate();

app.MapGrpcService<OrganizationsGrpcService>();
app.MapCodeFirstGrpcReflectionService();

await app.RunAsync();