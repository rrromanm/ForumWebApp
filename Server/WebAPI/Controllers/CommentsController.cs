using DTOs.Comment;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
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

        // POST localhost:7078/comments
        [HttpPost]
        public async Task<IResult> CreateCommentAsync([FromBody] AddComentDTO    request)
        {
            var existingPost = await _postRepository.GetSingleAsync(request.PostId);
            var existingUser = await _userRepository.GetSingleAsync(request.UserId);

            if (existingPost == null)
            {
                return Results.NotFound($"Post with ID '{request.PostId}' not found.");
            }

            if (existingUser == null)
            {
                return Results.NotFound($"User with ID '{request.UserId}' not found.");
            }

            var comment = new Comment
            {
                Body = request.Body,
                UserId = request.UserId,
                PostId = request.PostId
            };
            await _commentRepository.AddAsync(comment);
            return Results.Created($"comments/{comment.Id}", comment);
        }

        // PUT localhost:7078/comments/{id}
        [HttpPut("{id:int}")]
        public async Task<IResult> UpdateCommentAsync([FromRoute] int id, [FromBody] ReplaceCommentDTO request)
        {
            var existingComment = await _commentRepository.GetSingleAsync(id);
            if (existingComment == null)
            {
                return Results.NotFound($"Comment with ID {id} not found.");
            }

            var comment = new Comment
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
        [HttpGet("{id:int}")]
        public async Task<IResult> GetSingleCommentAsync([FromRoute] int id)
        {
            var result = await _commentRepository.GetSingleAsync(id);
            if (result == null)
            {
                return Results.NotFound($"Comment with ID {id} not found.");
            }
            return Results.Ok(result);
        }

        // GET localhost:7078/comments
        [HttpGet]
        public async Task<IResult> GetCommentsAsync([FromQuery] string? title, [FromQuery] int? commentId)
        {
            List<Comment> comments = _commentRepository.GetManyAsync().ToList();
            if(!string.IsNullOrWhiteSpace(title))
            {
                comments = comments.Where(c => c.Body.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if(commentId.HasValue)
            {
                comments = comments.Where(c => c.Id == commentId).ToList();
            }
            return Results.Ok(comments);
        }

        // DELETE localhost:7078/comments/{id}
        [HttpDelete("{id:int}")]
        public async Task<IResult> DeleteCommentAsync([FromRoute] int id)
        {
            var existingComment = await _commentRepository.GetSingleAsync(id);
            if (existingComment == null)
            {
                return Results.NotFound($"Comment with ID {id} not found.");
            }

            await _commentRepository.DeleteAsync(id);
            return Results.NoContent();
        }
    }
}
