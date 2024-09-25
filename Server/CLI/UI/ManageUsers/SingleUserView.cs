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

    public async Task SingleUserAsync()
    {
        Console.Clear();
        Console.WriteLine(
            "Enter username ID which you would like to view: ");
        if (int.TryParse(Console.ReadLine(), out int userIdToView))
        {
            User? user = await userRepository.GetSingleAsync(userIdToView);
            Console.WriteLine($"ID: {user.Id}, username: {user.Username}");
        }
        
    }

}