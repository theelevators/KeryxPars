using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KeryxPars.MessageViewer.Client;
using KeryxPars.MessageViewer.Client.Services;
using KeryxPars.MessageViewer.Core.Interfaces;
using KeryxPars.MessageViewer.Core.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IMessageParserService, MessageParserService>();
builder.Services.AddScoped<IMessageRepository, InMemoryMessageRepository>();
builder.Services.AddSingleton<ValidationProfileService>();

await builder.Build().RunAsync();
