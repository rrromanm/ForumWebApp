using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    
    private readonly IUserRepository userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task UpdateUserAsync(string username, string password, int userId)
    {
        User user = new User(username, password, userId);
        await userRepository.UpdateAsync(user);
        Console.WriteLine($"User with ID {userId} updated successfully.");
    }

    public async Task DeleteUserAsync(int userId)
    {
        await userRepository.DeleteAsync(userId);
        Console.WriteLine($"User with ID {userId} deleted successfully.");
    }
}