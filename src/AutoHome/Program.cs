using AutoHome;
using AutoHome.Data;
using Microsoft.EntityFrameworkCore;
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

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<SqliteDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

    builder.Services.AddScoped<ICurtainsDataManager, CurtainsDataManager>();
    builder.Services.AddSingleton<ICurtainController, CurtainController>();

    builder.Services.AddSingleton<ITimeTriggersService, TimeTriggersService>();
    builder.Services.AddHostedService<TriggerLoaderHostedService>();

    CertificateOptions certOptions = new();
    builder.Configuration.GetSection(CertificateOptions.Section).Bind(certOptions);
    if (!builder.Environment.IsDevelopment())
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5000, listenOptions =>
            {
                listenOptions.UseHttps(certOptions.Path);
            });
        }).UseUrls("https://ac.pi.lan");
    }

    var app = builder.Build();

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
