using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly IUserRepository userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task DisplayUserAsync(int userId)
    { 
        User? user = await userRepository.GetSingleAsync(userId);
        Console.WriteLine($"ID: {user.Id}, username: {user.Username}");
    }
}