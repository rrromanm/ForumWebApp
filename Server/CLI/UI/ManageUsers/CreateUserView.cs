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

    public async Task addUserAsync(string username, string password)
    {
        User user = new User(username, password);
        await userRepository.AddAsync(user);
        Console.WriteLine($"User '{username}' has been created successfully.");
    }
}