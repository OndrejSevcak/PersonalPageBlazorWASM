using Microsoft.Extensions.Options;
using PersonalPage.Models;
using System.Net.Http;
using System.Text.Json;

namespace PersonalPage.Services
{
    public class GitHubService
    {
        private readonly StorageSettings _storageSettings;
        private readonly HttpClient _httpClient;

        //public List<BlogPost> Posts { get; set; }

        public GitHubService(IOptions<StorageSettings> storageSettings, HttpClient httpClient)
        {
            _storageSettings = storageSettings.Value;
            _httpClient = httpClient;
        }

        //GET: /repos/{owner}/{repo}/contents/{path}
        public async Task<List<GitHubFile>?> GetFilesAsync(string folder)
        {
            var response = await _httpClient.GetAsync($"{_storageSettings.GitHubApiBaseUrl}/repos/{_storageSettings.GitHubUserName}/{_storageSettings.RepositoryName}/contents/{folder}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseContent))
                {
                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var files = JsonSerializer.Deserialize<List<GitHubFile>>(responseContent, jsonOptions);

                    if(files == null)
                    {
                        throw new Exception("No data returned from GitHub repository");
                    }

                    foreach (var file in files)
                    {
                        file.Content = await _httpClient.GetStringAsync(file.DownloadUrl);
                    }

                    return files;
                }
            }

            throw new Exception($"Could not retrieve posts from GitHub, status code: {response.StatusCode}, error message: {response.ReasonPhrase}");
        }
    }
}
