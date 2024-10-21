using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public CommentsController(ICommentRepository commentRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }
    //POST http://localhost:
}