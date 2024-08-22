using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PersonalPage;
using PersonalPage.Services.Blog;
using PersonalPage.Services.Tetris.Demo1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//http client
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//custom services
builder.Services.AddScoped<GameService>();

//options pattern
builder.Services.AddOptions<StorageSettings>().Configure(options =>
{
    options.DataPath = "JsonStorage";
    options.PostsFolder = "Posts";
    options.CategoriesFolder = "Categories";
});

builder.Services.AddScoped<IStorageService, StorageService>();

//Auth0 OIDC authentication
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();
