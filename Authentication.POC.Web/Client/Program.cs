using Authentication.POC.Web.Client;
using Authentication.POC.Web.Client.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var client = new HttpClient();
client.BaseAddress = new Uri("localhost:5001");

builder.Services.AddScoped(sp => client);

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TestAuthProviders>();

await builder.Build().RunAsync();
