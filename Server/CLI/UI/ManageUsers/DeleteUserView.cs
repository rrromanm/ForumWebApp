using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository _userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task DeleteUserAsync()
    {
        Console.Clear();
        Console.WriteLine(
            "Enter username ID which you would like to delete: ");
        if (int.TryParse(Console.ReadLine(), out int userIdToDelete))
        {
            Console.WriteLine($"User with ID {userIdToDelete} deleted successfully.");
            await _userRepository.DeleteAsync(userIdToDelete);
        }
        
    }
}