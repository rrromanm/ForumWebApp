using DTOs.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostsController(ICommentRepository commentRepository,
        IUserRepository userRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    //POST localhost:7078/posts
    [HttpPost]
    public async Task<IResult> CreatePost([FromBody] AddPostDTO request)
    {
        User? existingUser =
            await _userRepository.GetSingleAsync(request.UserId);

        if (existingUser == null)
        {
            return Results.NotFound(
                $"User with ID '{request.UserId}' not found.");
        }

        Post post = new Post()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId
        };
        
        await _postRepository.AddAsync(post);
        return Results.Created($"posts/{post.Id}", post);
    }

    // PUT localhost:7078/posts/{id}
    public async Task<IResult> UpdatePost([FromRoute] int id,
        [FromBody] ReplacePostDTO request)
    {
        Post? existingPost = await _postRepository.GetSingleAsync(id);
        Post post = new Post()
        {
            Id = id,
            Title = request.Title,
            Body = request.Body,
            UserId = existingPost.UserId
        };
        await _postRepository.UpdateAsync(post);
        return Results.Ok(post);
    }

    // GET localhost:7078/posts/{id}
    [HttpGet("{id:int}")]
    public async Task<IResult> GetSinglePost([FromRoute] int id)
    {
        try
        {
            Post? result = await _postRepository.GetSingleAsync(id);
            if (result == null)
            {
                return Results.NotFound($"Post with ID {id} not found.");
            }

            List<Comment> comments = await _commentRepository.GetCommentsByPostIdAsync(id);
            var postWithComments = new
            {
                Post = result,
                Comments = comments
            };
            return Results.Ok(postWithComments);
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }

    }

    // GET localhost:7078/posts
    [HttpGet]
    public async Task<IResult> GetPosts([FromQuery] string? titleContains,
        [FromQuery] int? userId)
    {
        List<Post> posts = _postRepository.GetMany().ToList();
        
        if (string.IsNullOrEmpty(titleContains))
        {
            posts = posts.Where(c =>
                    c.Body.Contains(titleContains, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (userId.HasValue)
        {
            posts = posts.Where(p => p.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (userId.HasValue)
        {
            posts = posts.Where(p => p.UserId == userId).ToList();
        }

        return Results.Ok(posts);
    }

    // DELETE localhost:7078/posts/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeletePostAsync(int id)
    {
        await _postRepository.DeleteAsync(id);
        return Results.NoContent();
    }
}