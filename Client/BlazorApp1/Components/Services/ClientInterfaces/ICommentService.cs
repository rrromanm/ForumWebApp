using Entities;

namespace BlazorApp1.Components.Services.ClientInterfaces;

public interface ICommentService
{
    Task AddCommentAsync(int postId, string body);
    Task<ICollection<Comment>> GetCommentsByPostIdAsync(int postId);
}