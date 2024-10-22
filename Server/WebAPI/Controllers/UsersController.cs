using DTOs.User;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController
{
    private readonly IUserRepository _userRepository;
    
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //POST localhost:7078/users
    [HttpPost]
    public async Task<IResult> CreateUser([FromBody] AddUserDTO request)
    {
        User user = new User()
        {
            Username = request.username,
            Password = request.password,
            Id = request.Id
        };
        await _userRepository.AddAsync(user);
        return Results.Created($"users/{user.Id}", user);
    } 

    // PUT localhost:7078/users/{id}
    public async Task<IResult> UpdateUser([FromRoute] int id, [FromBody] ReplaceUserDTO request)
    {
        User user = new User()
        {
            Username = request.username,
            Password = request.password,
            Id = id
        };
        await _userRepository.UpdateAsync(user);
        return Results.Ok();
    }

    // GET localhost:7078/users/{id}
    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User result = await _userRepository.GetSingleAsync(id);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }

    // GET localhost:7078/users/
    [HttpGet]
    public async Task<IResult> GetUsers([FromQuery] string? username, [FromQuery] int? userId)
    {
        List<User> users = _userRepository.GetMany().ToList();
        if (!string.IsNullOrWhiteSpace(username))
        {
            users = users.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (userId.HasValue)
        {
            users = users.Where(u => u.Id == userId).ToList();
        }

        return Results.Ok(users);

    }

    // DELETE localhost:7078/users/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteUser(int id)
    {
        await _userRepository.DeleteAsync(id);
        return Results.NoContent();
    }
}