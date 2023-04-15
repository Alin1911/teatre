using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobyLabWebProgramming.Infrastructure.Configurations;
using MobyLabWebProgramming.Infrastructure.Database;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobyLabWebProgramming.Infrastructure.Converters;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using MobyLabWebProgramming.Infrastructure.Workers;
using Serilog;
using Serilog.Events;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Repositories.Implementation;

namespace MobyLabWebProgramming.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// This extension method adds the database configuration and repository to the application builder.
    /// </summary>
    public static WebApplicationBuilder AddRepository(this WebApplicationBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // This is used to avoid some errors with the timezone when working with timestamps.

        builder.Services.AddDbContext<WebAppDatabaseContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("WebAppDatabase"), // This gets the connection string from ConnectionStrings.WebAppDatabase in appsettings.json.
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
                    .CommandTimeout((int)TimeSpan.FromMinutes(15).TotalSeconds)));
        builder.Services.AddTransient<IRepository<WebAppDatabaseContext>, Repository<WebAppDatabaseContext>>();

        return builder;
    }

    /// <summary>
    /// This extension method adds the CORS configuration to the application builder.
    /// </summary>
    public static WebApplicationBuilder AddCorsConfiguration(this WebApplicationBuilder builder)
    {
        var corsConfiguration = builder.Configuration.GetSection(nameof(CorsConfiguration)).Get<CorsConfiguration>();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policyBuilder =>
                {
                    policyBuilder.WithOrigins(corsConfiguration.Origins) // This adds the valid origins that the browser client can have.
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        return builder;
    }

    /// <summary>
    /// This extension method adds the controllers and JSON serialization configuration to the application builder.
    /// </summary>
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer()
            .AddMvc()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Adds a conversion by name of the enums, otherwise numbers representing the enum values are used.
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // This converts the public property names of the objects serialized to Camel case.
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // When deserializing request the properties of the JSON are mapped ignoring the casing.
            });

        return builder;
    }

    /// <summary>
    /// This extension method adds the default authorization policy to the AuthorizationPolicyBuilder.
    /// It requires that the JWT needs to have the given claims in the configuration.
    /// </summary>
    private static AuthorizationPolicyBuilder AddDefaultPolicy(this AuthorizationPolicyBuilder policy) =>
        policy.RequireClaim(ClaimTypes.NameIdentifier)
            .RequireClaim(ClaimTypes.Name)
            .RequireClaim(ClaimTypes.Email);


    /// <summary>
    /// This extension method adds just the authorization configuration to the application builder.
    /// </summary>
    private static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // This is to use the JWT token with the "Bearer" scheme
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtConfiguration = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>(); // Here we use the JWT configuration from the application.json.

                var key = Encoding.ASCII.GetBytes(jwtConfiguration.Key); // Use configured key to verify the JWT signature.
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true, // Validate the issuer claim in the JWT. 
                    ValidateAudience = true, // Validate the audience claim in the JWT.
                    ValidAudience = jwtConfiguration.Audience, // Sets the intended audience.
                    ValidIssuer = jwtConfiguration.Issuer, // Sets the issuing authority.
                    ClockSkew = TimeSpan.Zero // No clock skew is added, when the token expires it will immediately become unusable.
                };
                options.RequireHttpsMetadata = false;
                options.IncludeErrorDetails = true;
            }).Services
            .AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().AddDefaultPolicy().Build(); // Adds the default policy for the JWT claims.
            });

        return builder;
    }

    /// <summary>
    /// This extension method adds the authorization with the Swagger configuration to the application builder.
    /// </summary>
    public static WebApplicationBuilder AddAuthorizationWithSwagger(this WebApplicationBuilder builder, string application)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<SmartEnumSchemaFilter>();
            c.SwaggerDoc("v1", new() { Title = application, Version = "v1" }); // Adds the application name and version, there can be more than one version for the API.
            c.AddSecurityDefinition("Bearer", new() // This is to configure the authorization in the Swagger client so that you may test authorized routes.
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            c.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return builder.ConfigureAuthentication();
    }

    /// <summary>
    /// This extension method adds any necessary services to the application builder that need to be injected by the framework.
    /// </summary>
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
        builder.Services.Configure<FileStorageConfiguration>(builder.Configuration.GetSection(nameof(FileStorageConfiguration)));
        builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection(nameof(MailConfiguration)));
        builder.Services
            .AddTransient<IUserService, UserService>()
            .AddTransient<IActorService, ActorService>()
            .AddTransient<IPerformanceService, PerformanceService>()
            .AddTransient<IPieceService, PieceService>()
            .AddTransient<IRoleService, RoleService>()
            .AddTransient<ITicketService, TicketService>()
            .AddTransient<ITransactionService, TransactionService>()
            .AddTransient<IHallService, HallService>()
            .AddTransient<ILoginService, LoginService>()
            .AddTransient<IFileRepository, FileRepository>()
            .AddTransient<IMailService, MailService>();

        return builder;
    }

    /// <summary>
    /// This extension method adds the advanced logging configuration to the application builder.
    /// </summary>
    public static WebApplicationBuilder UseLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, logger) =>
        {
            logger
                .MinimumLevel.Is(LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .WriteTo.Console();
        });

        return builder;
    }

    /// <summary>
    /// This extension method adds asynchronous workers to the application builder.
    /// </summary>
    public static WebApplicationBuilder AddWorkers(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<InitializerWorker>();

        return builder;
    }
}
