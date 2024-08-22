using Microsoft.Extensions.Options;
using PersonalPage.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;

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

    //repos/{owner}/{repo}/ contents /{path}
    //PUT
    public async Task SaveBlogPostAsJsonToGithubStorageAsync(BlogPost post)
    {
        var owner = _settings.GitHubUserName;
        var repo = _settings.RepositoryName;
        var path = $"{_settings.DataPath}/{_settings.PostsFolder}/post_{post.Id}.json";
        var url = $"{_settings.GitHubApiBaseUrl}repos/{owner}/{repo}/contents/{path}";

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
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(fileContent), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, jsonContent);

            response.EnsureSuccessStatusCode();
        }
    }

    //repos/{owner}/{repo}/contents/{path}
    //GET
    public async Task<List<BlogPost>?> LoadBlogPostsFromGitHubStorage()
    {
        var owner = _settings.GitHubUserName;
        var repo = _settings.RepositoryName;
        var path = $"{_settings.DataPath}/{_settings.PostsFolder}";
        var url = $"{_settings.GitHubApiBaseUrl}repos/{owner}/{repo}/contents/{path}";

        List<BlogPost>? posts = new();

        string? githubToken = _config.GetValue<string>("GitHubApiAccessToken");

        if (String.IsNullOrEmpty(githubToken))
        {
            throw new Exception("Could not retrieve GitHub access token from configuration file");
        }

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", githubToken);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            if (String.IsNullOrEmpty(content))
            {
                posts = JsonSerializer.Deserialize<List<BlogPost>>(content);
            }
        }

        return posts;
    }

}
