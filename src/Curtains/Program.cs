using AutoHome.Hardware;
using Curtains;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.ComponentModel.DataAnnotations;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

try
{
    Log.Information("Starting host");

    var builder = WebApplication.CreateBuilder(args);

    // Auth
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
    builder.Services.AddAuthorization();
    builder.Services.AddTransient<ITokenService, TokenService>();

    // Add OpenAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new()
        {
            Title = builder.Environment.ApplicationName,
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    });

    builder.Services.AddOptions<CurtainConfig>()
        .Bind(builder.Configuration.GetSection(CurtainConfig.Section))
        .ValidateDataAnnotations();
    builder.Services.AddAutoCurtainsHardware(builder.Configuration);

    var app = builder.Build();

    app.Urls.Add("http://*:5000");

    if (app.Environment.IsDevelopment() || builder.Configuration["Swagger"] == "Enabled")
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
            builder.Environment.ApplicationName));
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapPost("/token", [AllowAnonymous] async (HttpContext http, ITokenService tokenService) => {
        var deviceId = await http.Request.ReadFromJsonAsync<Auth>();
        if (deviceId?.DeviceId != builder.Configuration["DeviceId"])
        {
            return Results.Unauthorized();
        }

        var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"]);
        return Results.Ok(new { token = token });
    })
    .Accepts(typeof(Auth), "application/json")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status500InternalServerError);

    app.MapPost("/open", [Authorize] (IStepperMotor motor, IOptions<CurtainConfig> options) =>
    {
        app.Logger.LogInformation("Opening...");
        try
        {
            motor.SetEnabledState(true);
            motor.RPM = options.Value.RPM;
            motor.Step(options.Value.Steps);
        }
        catch(Exception ex)
        {
            app.Logger.LogError(ex, "Error opening");
            return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
        finally
        {
            motor.SetEnabledState(false);
            app.Logger.LogInformation("Opened");
        }
        return Results.Ok();
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status500InternalServerError);

    app.MapPost("/close", [Authorize] (IStepperMotor motor, IOptions<CurtainConfig> options) =>
    {
        app.Logger.LogInformation("Closing...");
        try
        {
            motor.SetEnabledState(true);
            motor.RPM = options.Value.RPM;
            motor.Step(-options.Value.Steps);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "Error opening");
            return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
        finally
        {
            motor.SetEnabledState(false);
            app.Logger.LogInformation("Closed");
        }
        return Results.Ok();
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status500InternalServerError);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Host ended, flushing log...");
    Log.CloseAndFlush();
}

namespace Curtains
{
    public record Auth(string DeviceId);

    public class CurtainConfig
    {
        public const string Section = nameof(CurtainConfig);

        [Required]
        public int Steps { get; set; }

        [Required]
        public int RPM { get; set; }
    }
}
