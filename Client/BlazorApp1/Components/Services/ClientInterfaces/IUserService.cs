using DTOs.User;

namespace BlazorApp1.Components.Services.ClientInterfaces;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(AddUserDTO addUserDto);
    public Task UpdateUserAsync(int id, ReplaceUserDTO replaceUserDto);
    Task<string> GetUserNameByIdAsync(int userId);
}