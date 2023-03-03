using App.Web.Config;
using App.Web.Services;
using App.Web.Stores;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App.Web.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// localstorage service
builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(ApiOptions.Host) });

builder.Services.AddTransient<IApiService, ApiService>();

// stores
builder.Services.AddSingleton<ITokenStore, TokenStore>();
builder.Services.AddSingleton<ISessionStore, SessionStore>();

await builder.Build().RunAsync();
