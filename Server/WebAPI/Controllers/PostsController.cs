using DTOs.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
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

        // POST localhost:7078/posts
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync(AddPostDTO request)
        {
            var post = new Post
            {
                Title = request.Title,
                Body = request.Body,
                UserId = request.UserId // Ensure UserId is assigned
            };

            try
            {
                var createdPost = await _postRepository.AddAsync(post);
                return Ok(createdPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT localhost:7078/posts/{id}
        [HttpPut("{id:int}")]
        public async Task<IResult> UpdatePostAsync([FromRoute] int id, [FromBody] ReplacePostDTO request)
        {
            var existingPost = await _postRepository.GetSingleAsync(id);
            if (existingPost == null)
            {
                return Results.NotFound($"Post with ID {id} not found.");
            }

            existingPost.Title = request.Title;
            existingPost.Body = request.Body;

            await _postRepository.UpdateAsync(existingPost);
            return Results.Ok(existingPost);
        }

        // GET localhost:7078/posts/{id}
        [HttpGet("{id:int}")]
        public async Task<IResult> GetSinglePostAsync([FromRoute] int id)
        {
            try
            {
                var result = await _postRepository.GetSingleAsync(id);
                if (result == null)
                {
                    return Results.NotFound($"Post with ID {id} not found.");
                }

                var comments = await _commentRepository.GetCommentsByPostIdAsync(id);
                var postWithComments = new
                {
                    Post = result,
                    Comments = comments
                };
                return Results.Ok(postWithComments);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework)
                return Results.StatusCode(500);
            }
        }

        // GET localhost:7078/posts
        [HttpGet]
        public async Task<IResult> GetPostsAsync([FromQuery] string? titleContains, [FromQuery] int? userId)
        {
            List<Post> posts =  _postRepository.GetMany().ToList();

            if (!string.IsNullOrWhiteSpace(titleContains))
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
            var existingPost = await _postRepository.GetSingleAsync(id);
            if (existingPost == null)
            {
                return Results.NotFound($"Post with ID {id} not found.");
            }

            await _postRepository.DeleteAsync(id);
            return Results.NoContent();
        }
        
        // GET https://localhost:7078/posts/{postId}/comments
        [HttpGet("{postId:int}/comments")]
        public async Task<IResult> GetCommentsByPostIdAsync([FromRoute] int postId)
        {
            try
            {
                List<Comment> comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
                return Results.Ok(comments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.NotFound(e.Message);
            }
        }
    }
}
