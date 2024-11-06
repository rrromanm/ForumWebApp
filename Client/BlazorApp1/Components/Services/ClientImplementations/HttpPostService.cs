using System.Text.Json;
using BlazorApp1.Components.Services.ClientInterfaces;
using DTOs.Post;
using Entities;

namespace BlazorApp1.Components.Services.ClientImplementations;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;
    
    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }
    
    
    public async Task CreateAsync(AddPostDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7078/Posts", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetPostsAsync(string? titleContains, string? contentContains, string? username)
    {
        string query = ConstructQuery(titleContains, contentContains, username);
        HttpResponseMessage response = await client.GetAsync("https://localhost:7078/Posts" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    private string ConstructQuery(string? titleContains, string? contentContains, string? username)
    {
        string query = "";
        if (titleContains != null)
        {
            query += $"titleContains={titleContains}&";
        }

        if (contentContains != null)
        {
            query += $"contentContains={contentContains}&";
        }
        if (username != null)
        {
            query += $"username={username}&";
        }

        return query;
    }
}