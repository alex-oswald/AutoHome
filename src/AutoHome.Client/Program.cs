using AutoHome.Client;
//using AutoHome.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
//using Curtains.Plugin;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddClientDataManagers();

builder.Services.AddMudServices();

builder.Services.AddAutoMapper(typeof(Program));

// PLUGINS
//builder.Services.AddCurtainsPlugin();

await builder.Build().RunAsync();
