using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp1.Components.Services.ClientInterfaces;
using Entities;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    public async Task AddCommentAsync(int postId, string body)
    {
        var newComment = new { PostId = postId, Body = body };
        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7078/Comments", newComment);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7078/Comments?postId={postId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;
    }
}