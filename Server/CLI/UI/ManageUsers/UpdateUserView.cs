using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository _userRepository;

    public UpdateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task UpdateUserAsync()
    {
        Console.Clear();
        Console.WriteLine(
            "Enter username ID which you would like to update: ")
            ;
        if (int.TryParse(Console.ReadLine(), out int userIdToUpdate))
        {
            Console.Write("Enter New Username: ");
            var newUsername = Console.ReadLine();
            Console.Write("Enter New Password: ");
            var newPassword = Console.ReadLine();
            User user = new User(newUsername, newPassword, userIdToUpdate);
            Console.WriteLine($"User with ID {userIdToUpdate} updated successfully.");
            await _userRepository.UpdateAsync(user);
        }
        
        
        
    }
    
}