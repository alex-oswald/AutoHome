using AutoHome.Data;
using AutoHome.Server;
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

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<SqliteDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
    builder.Services.AddScoped<IAsyncRepository<Device>, EntityFrameworkRepository<Device, SqliteDbContext>>();
    builder.Services.AddScoped<IAsyncRepository<Trigger>, EntityFrameworkRepository<Trigger, SqliteDbContext>>();

    builder.Services.AddScoped<ICurtainsService, CurtainsService>();

    builder.Services.AddSingleton<ITriggersService, TriggersService>();
    builder.Services.AddHostedService<TriggerLoaderHostedService>();

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

    // Make sure the database is created
    using (var scoped = app.Services.CreateScope())
    {
        var db = scoped.ServiceProvider.GetService<SqliteDbContext>();
        db?.Database.EnsureCreated();
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
