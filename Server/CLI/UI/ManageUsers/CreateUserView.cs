using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task addUserAsync()
    {
        Console.Clear();
        Console.WriteLine("Enter username: ");
        var username = Console.ReadLine();
        Console.WriteLine("Enter password: ");
        var password = Console.ReadLine();
        User user = new User(username, password);
        Console.WriteLine($"User '{username}' has been created successfully.");
        await userRepository.AddAsync(user);
    }
}