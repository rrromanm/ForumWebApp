using BlazorApp1.Components.Services.ClientInterfaces;
using DTOs.User;
using Entities;

namespace BlazorApp1.Components.Services.ClientImplementations;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;
    
    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<string> GetUserNameByIdAsync(int userId)
    {
        var response = await client.GetAsync($"api/users/{userId}");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<User>();
        return user?.Username;
    }
    
    public Task<UserDTO> AddUserAsync(AddUserDTO addUserDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(int id, ReplaceUserDTO replaceUserDto)
    {
        throw new NotImplementedException();
    }
}