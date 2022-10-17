using AutoHome.DeviceCore;
using AutoHome.Hardware;
using Curtains.Device;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

IntegrationBuilder integrationBuilder = new();

integrationBuilder.Run(args,
(services, config) =>
{
    services.AddOptions<CurtainConfig>()
        .Bind(config.GetSection(CurtainConfig.Section))
        .ValidateDataAnnotations();

    // Hardware
    services.AddAutoHomeHardware(config);
},
app =>
{
    app.MapPost("/open", [Authorize] (IStepperMotor motor, IOptions<CurtainConfig> options) =>
    {
        app.Logger.LogInformation("Opening...");
        try
        {
            motor.SetEnabledState(true);
            motor.RPM = options.Value.RPM;
            motor.Step(options.Value.Steps);
        }
        catch (Exception ex)
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
});
