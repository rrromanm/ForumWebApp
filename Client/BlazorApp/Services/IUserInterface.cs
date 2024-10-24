using DTOs.User;

namespace BlazorApp.Services;

public interface IUserInterface
{
    public Task<AddUserDTO> AddUserAsync(AddUserDTO request);
    
}