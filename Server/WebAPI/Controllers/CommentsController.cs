using DTOs.Comment;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CommentsController(ICommentRepository commentRepository,
        IUserRepository userRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    //POST localhost:7078/comments
    [HttpPost]
    public async Task<IResult> CreateComment([FromBody] AddComentDTO request)
    {
        Post? existingPost =
            await _postRepository.GetSingleAsync(request.PostId);
        User? existingUser =
            await _userRepository.GetSingleAsync(request.UserId);

        if (existingPost == null)
        {
            return Results.NotFound(
                $"Post with ID '{request.PostId}' not found.");
        }

        if (existingUser == null)
        {
            return Results.NotFound(
                $"User with ID '{request.UserId}' not found.");
        }

        Comment comment = new Comment()
        {
            Body = request.Body,
            UserId = request.UserId,
            PostId = request.PostId
        };
        await _commentRepository.AddAsync(comment);
        return Results.Created($"comments/{comment.Id}", comment);
    }

    // PUT localhost:7078/comments/{id}
    public async Task<IResult> UpdateComment([FromRoute] int id,
        [FromBody] ReplaceCommentDTO request)
    {
        Comment? existingComment = await _commentRepository.GetSingleAsync(id);
        Comment comment = new Comment()
        {
            Id = id,
            Body = request.Body,
            UserId = existingComment.UserId,
            PostId = existingComment.PostId
        };
        await _commentRepository.UpdateAsync(comment);
        return Results.Ok(comment);
    }

    // GET localhost:7078/comments/{id}
    [HttpGet]
    public async Task<IResult> GetSingleComment([FromRoute] int id)
    {
        try
        {
            Comment result = await _commentRepository.GetSingleAsync(id);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }

    }

    // GET localhost:7078/comments/post/
    [HttpGet]
    public async Task<IResult> GetComments([FromQuery] string? title,
        [FromQuery] int? commentId)
    {
        List<Comment> comments = new List<Comment>();
        if (string.IsNullOrEmpty(title))
        {
            comments = comments.Where(c =>
                    c.Body.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (commentId.HasValue)
        {
            comments = comments.Where(c => c.Id == commentId).ToList();
        }

        return Results.Ok(comments);
    }

    // DELETE localhost:7078/comments/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteComment([FromRoute] int id)
    {
        await _commentRepository.DeleteAsync(id);
        return Results.NoContent();
    }
}