using DTOs.User;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST localhost:7078/users
        [HttpPost]
        public async Task<IResult> CreateUserAsync([FromBody] AddUserDTO request)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetByUsernameAsync(request.username);
            if (existingUser != null)
            {
                return Results.Conflict("Username is already in use.");
            }

            // Create the new user
            var user = new User
            {
                Username = request.username,
                Password = request.password,
                Id = request.Id
            };

            await _userRepository.AddAsync(user);
            return Results.Created($"users/{user.Id}", user);
        }

        // PUT localhost:7078/users/{id}
        [HttpPut("{id:int}")]
        public async Task<IResult> UpdateUserAsync([FromRoute] int id, [FromBody] ReplaceUserDTO request)
        {
            // Check if the user exists
            var existingUser = await _userRepository.GetSingleAsync(id);
            if (existingUser == null)
            {
                return Results.NotFound("User not found.");
            }

            // Check if the new username is already taken by another user
            if (existingUser.Username != request.username)
            {
                var usernameTaken = await _userRepository.GetByUsernameAsync(request.username);
                if (usernameTaken != null)
                {
                    return Results.Conflict("Username is already in use.");
                }
            }

            // Update the user
            existingUser.Username = request.username;
            existingUser.Password = request.password;

            await _userRepository.UpdateAsync(existingUser);
            return Results.Ok(existingUser);
        }

        // GET localhost:7078/users/{id}
        [HttpGet("{id:int}")]
        public async Task<IResult> GetSingleUserAsync([FromRoute] int id)
        {
            try
            {
                var result = await _userRepository.GetSingleAsync(id);
                if (result == null) return Results.NotFound("User not found.");
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework)
                return Results.StatusCode(500);
            }
        }

        // GET localhost:7078/users/
        [HttpGet]
        public async Task<IResult> GetUsersAsync([FromQuery] string? username, [FromQuery] int? userId)
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
        public async Task<IResult> DeleteUserAsync(int id)
        {
            try
            {
                var existingUser = await _userRepository.GetSingleAsync(id);
                if (existingUser == null)
                {
                    return Results.NotFound("User not found.");
                }

                await _userRepository.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return Results.StatusCode(500);
            }
        }
    }
}
