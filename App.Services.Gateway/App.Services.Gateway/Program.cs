using App.Infrastructure.Extensions;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using App.Infrastructure.Options;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Tournaments.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Billing.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Gateway.Infrastructure.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();

// Add services to the container.

builder.Services.RegisterOptions();

builder.Services.AddRabbitMq();

builder.Services.AddGrpcServiceClient<IUsersGrpcService>();
builder.Services.AddGrpcServiceClient<IDepartmentsGrpcService>();
builder.Services.AddGrpcServiceClient<IOrganizationsGrpcService>();
builder.Services.AddGrpcServiceClient<ITeamsGrpcService>();
builder.Services.AddGrpcServiceClient<IEventsGrpcService>();
builder.Services.AddGrpcServiceClient<IGamesGrpcService>();
builder.Services.AddGrpcServiceClient<ITournamentsGrpcService>();
builder.Services.AddGrpcServiceClient<IOrdersGrpcService>();
builder.Services.AddGrpcServiceClient<ITicketGrpcService>();
builder.Services.AddGrpcServiceClient<IBillingGrpcService>();
builder.Services.AddGrpcServiceClient<IAuthenticationGrpcService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "EUC Syd ESport Association",
        Version = "v1"
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "**ONLY** insert your token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        },
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddSingleton<IIssuerSigningKeyCache, IssuerSigningKeyCache>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCustomJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = JwtOptions.Issuer,
            ValidAudience = JwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key)),
        };
    });

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("SuperAdmin");
//});

var app = builder.Build();

app.UseCors();

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