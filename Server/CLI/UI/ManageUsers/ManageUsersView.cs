using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ManageUsersView(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        
    }

    public async Task displayMenu()
    {
        while (true)
        {
            Console.WriteLine("Manage users: ");
            Console.WriteLine("1. Create an user");
            Console.WriteLine("2. Update an user");
            Console.WriteLine("3. Delete an user");
            Console.WriteLine("4. View all users");
            Console.WriteLine("5. View single user");
            Console.WriteLine("0. Back");
            Console.Write("Select an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreateUserAsync();
                    break;

                case "2":
                    await UpdateUserAsync();
                    break;

                case "3":
                    await DeleteUserAsync();
                    break;

                case "4":
                    await ListUsersAsync();
                    break;

                case "5":
                    await SingleUserAsync();
                    break;
                case "0":
                    await MainMenuAsync();
                    break;
                default:
                    Console.WriteLine("Incorrect input"); break;
            }
        }
        
    }
    private async Task CreateUserAsync()
    {
        var createUserView = new CreateUserView(userRepository);
        await createUserView.addUserAsync();
    }

    private async Task UpdateUserAsync()
    {
        var updateUserView = new UpdateUserView(userRepository);
        await updateUserView.UpdateUserAsync();
    }

    private async Task DeleteUserAsync()
    {
        var deleteUserView = new DeleteUserView(userRepository);
        await deleteUserView.DeleteUserAsync();
    }

    private async Task ListUsersAsync()
    {
        var listUsersView = new ListUsersView(userRepository);
        await listUsersView.ListUsersAsync();
    }

    private async Task SingleUserAsync()
    {
        var singleUserView = new SingleUserView(userRepository);
        await singleUserView.SingleUserAsync();
    }
    
    private async Task MainMenuAsync()
    {
        var mainMenu = new CliApp(userRepository, postRepository, commentRepository);
        await mainMenu.StartAsync();
    }
}