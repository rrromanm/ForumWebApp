using DTOs.Comment;
using DTOs.Post;
using Entities;

namespace BlazorApp1.Components.Services.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(AddPostDTO dto);
<<<<<<< Updated upstream

    Task<ICollection<Post>> GetPostsAsync(string title, string body,
        string? username);
    Task<PostWithCommentsDTO> GetPostByIdAsync(int id);
    Task<List<CommentDTO>> GetCommentsAsync(int postId);
    Task<AddComentDTO> AddCommentAsync(AddComentDTO dto);
=======
    Task<ICollection<PostDTO>> GetPostsAsync();
    Task<PostDTO> GetPostByIdAsync(int postId); // New method
>>>>>>> Stashed changes
}