using App.Infrastructure.Extensions;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using App.Infrastructure.Options;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

// Add services to the container.

builder.Services.RegisterOptions();

builder.Services.AddRabbitMq();

builder.Services.AddGrpcServiceClient<IUsersGrpcService>();
builder.Services.AddGrpcServiceClient<IDepartmentsGrpcService>();
builder.Services.AddGrpcServiceClient<IOrganizationsGrpcService>();
builder.Services.AddGrpcServiceClient<ITeamsGrpcService>();
builder.Services.AddGrpcServiceClient<IGamesGrpcService>();
builder.Services.AddGrpcServiceClient<IOrdersGrpcService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = JwtOptions.Issuer,
            ValidAudience = JwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key))
        };
    });

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

    if (!app.Environment.IsDevelopment())
    {
        // Emit debug-level events instead of the defaults
        options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;
    }

    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();