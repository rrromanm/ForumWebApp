using DTOs.User;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // POST localhost:7078/login
    [HttpPost]
    public async Task<IResult> LoginAsync([FromBody] LoginRequestDTO requestDto)
    {
        var user = await _userRepository.GetByUsernameAsync(requestDto.Username);
        if (user == null)
        {
            return Results.NotFound($"User with username '{requestDto.Username}' not found.");
        }

        if (user.Password != requestDto.Password)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(user);
    }

}