using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ListUsersAsync()
    {
        Console.Clear();
        Console.WriteLine("Listing all users");
        foreach (User user in userRepository.GetMany())
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}");
        }
    }
}