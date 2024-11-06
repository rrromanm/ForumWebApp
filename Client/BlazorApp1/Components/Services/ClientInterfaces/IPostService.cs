using DTOs.Post;
using Entities;

namespace BlazorApp1.Components.Services.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(AddPostDTO dto);
    Task<ICollection<Post>> GetPostsAsync(string title, string body, string? username);
}