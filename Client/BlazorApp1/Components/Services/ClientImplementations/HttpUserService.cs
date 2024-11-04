using BlazorApp1.Components.Services.ClientInterfaces;
using DTOs.User;

namespace BlazorApp1.Components.Services.ClientImplementations;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;
    
    public HttpUserService(HttpClient client)
    {
        this.client = client;
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