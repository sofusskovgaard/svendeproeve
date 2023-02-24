using System.Reflection;
using App.Data.Extensions;
using App.Data.Utilities;
using App.Infrastructure.Extensions;
using ProtoBuf.Grpc.Server;
using App.Services.Users.Infrastructure;
using App.Services.Organizations.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

builder.Services.RegisterOptions();

builder.Services.AddAutoMapper(Assembly.Load("App.Services.Users.Infrastructure"));

builder.Services.AddMongoDb();
builder.Services.AddRabbitMq(Assembly.Load("App.Services.Users.Infrastructure"));

// Add services to the container.
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var entityIndexGenerator = app.Services.GetRequiredService<IEntityIndexGenerator>();
await entityIndexGenerator.Generate(Assembly.Load("App.Services.Users.Data"));

app.MapGrpcService<UsersGrpcService>();
app.MapCodeFirstGrpcReflectionService();

await app.RunAsync();

// it may not be best practice and quite frankly horrifying to see
// assemblies specified using their names, but there's a purpose.
// that purpose is because we use a template to create each service.
// this way we can iterate and create new services at a much higher speed.
// - sofusskovgaard