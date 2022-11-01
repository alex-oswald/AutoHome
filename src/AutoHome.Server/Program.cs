using AutoHome.Data;
using AutoHome.Server.Services;
using Curtains.Plugin;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    //.MinimumLevel.Verbose()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning) // Comment to show SQL queries in the logs
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Debug() // Write to the Debug output in Visual Studio
    .CreateLogger();

try
{
    Log.Information("Starting host");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
        });
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<SqliteDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

    builder.Services.AddScoped<IAsyncRepository<Device>, EntityFrameworkRepository<Device, SqliteDbContext>>();
    builder.Services.AddScoped<IAsyncRepository<Trigger>, EntityFrameworkRepository<Trigger, SqliteDbContext>>();
    builder.Services.AddScoped<IAsyncRepository<TriggerEvent>, EntityFrameworkRepository<TriggerEvent, SqliteDbContext>>();
    builder.Services.AddScoped<IAsyncRepository<Variable>, EntityFrameworkRepository<Variable, SqliteDbContext>>();
    builder.Services.AddScoped<ITimeStampedRepository<TriggerEvent>, EntityFrameworkTimeStampedRepository<TriggerEvent, SqliteDbContext>>();

    builder.Services.AddSingleton<ITriggersService, TriggersService>();

    builder.Services.AddHostedService<TriggerLoaderHostedService>();

    builder.Services.AddDatabaseCleanupService();

    builder.Services.AddAmbientWeatherPluginServer();
    builder.Services.AddCurtainsPluginServer();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "AutoHome REST API",
        });
        options.EnableAnnotations();
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    // Create and/or migrate the database
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
        db.Database.Migrate();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

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
