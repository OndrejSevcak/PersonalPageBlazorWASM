using Microsoft.Extensions.Options;
using PersonalPage.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace PersonalPage.Services.Blog;

public class StorageService : IStorageService
{
    StorageSettings _settings;
    private readonly IConfiguration _config;

    public StorageService(IOptions<StorageSettings> option, IConfiguration config)
    {
        _settings = option.Value;
        _config = config;
    }

    public async Task SaveBlogPostAsJsonToGithubStorageAsync(BlogPost post)
    {
        var owner = "OndrejSevcak";
        var repo = "BlazorBlogStorage";
        var path = $"{_settings.PostsFolder}/post_{post.Id}.json";
        var url = $"https://api.github.com/repos/{owner}/{repo}/contents/{path}";

        string? githubToken = _config.GetValue<string>("GitHubApiAccessToken");

        if (String.IsNullOrEmpty(githubToken))
        {
            throw new Exception("Could not retrieve GitHub access token from configuration file");
        }

        using(HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", githubToken);

            var fileContent = new
            {
                message = "Creating/Updating JSON file",
                content = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(post)))
                // If updating, you'll also need to provide the `sha` of the file
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(fileContent), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, jsonContent);

            response.EnsureSuccessStatusCode();
        }
    }

}
