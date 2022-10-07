using AutoHome.Api;
using AutoHome.Client;
using Curtains.Plugin;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new AutoHomeRestClient(builder.HostEnvironment.BaseAddress, new HttpClient()));

builder.Services.AddMudServices();

builder.Services.AddAutoMapper(typeof(Program));

// Integration Plugins
builder.Services.AddCurtainsPlugin();

await builder.Build().RunAsync();
