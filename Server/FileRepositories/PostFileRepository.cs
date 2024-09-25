using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string FilePath = "posts.json";

    
    public PostFileRepository()
    {
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }
    }
    private async Task<List<Post>> LoadPostsAsync()
    {
        string postAsJson = await File.ReadAllTextAsync(FilePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;
        return posts;
    }
    private async Task SavePostsAsync(List<Post> posts)
    {
        string postAsJson = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(FilePath, postAsJson);
    }
    public async Task<Post> FindPostById(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? exisitingPost = posts.FirstOrDefault(p => p.Id == id);
        if (exisitingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}'not found.");
        }
        return exisitingPost;
    }
    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await LoadPostsAsync();
        post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        await SavePostsAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found.");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        await SavePostsAsync(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }
        
        posts.Remove(postToRemove);
        await SavePostsAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        return LoadPostsAsync().Result.AsQueryable();
    }
}