using System.Text.Json;
using BlazorApp1.Components.Services.ClientInterfaces;
using DTOs.Comment;
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

    public async Task<ICollection<PostDTO>> GetPostsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("https://localhost:7078/Posts");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        return JsonSerializer.Deserialize<ICollection<PostDTO>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

<<<<<<< Updated upstream
    public async Task<PostWithCommentsDTO> GetPostByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7078/Posts/{id}");
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API Response: {content}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return JsonSerializer.Deserialize<PostWithCommentsDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<List<CommentDTO>> GetCommentsAsync(int postId)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7078/Posts/{postId}/comments");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<CommentDTO>(); // Return an empty list if comments are not found
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CommentDTO>>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error fetching comments: " + ex.Message, ex);
        }
    }

    public async Task<AddComentDTO> AddCommentAsync(AddComentDTO dto)
    {
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"https://localhost:7078/Comments", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AddComentDTO>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error adding comment: " + ex.Message, ex);
        }
    }

    private string ConstructQuery(string? titleContains, string? contentContains, string? username)
=======
    public async Task<PostDTO> GetPostByIdAsync(int postId)
>>>>>>> Stashed changes
    {
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7078/Posts/{postId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return JsonSerializer.Deserialize<PostDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}