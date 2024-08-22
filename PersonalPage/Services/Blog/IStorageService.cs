using PersonalPage.Models;

namespace PersonalPage.Services.Blog;

public interface IStorageService
{
    Task SaveBlogPostAsJsonToGithubStorageAsync(BlogPost post);
    Task<List<BlogPost>?> LoadBlogPostsFromGitHubStorage();
}
