using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Manage Posts");
            Console.WriteLine("2. Manage Comments");
            Console.WriteLine("3. Manage Users");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": await ManagePostsAsync(); break;
                case "2" : await ManageCommentsAsync(); break;
                case "3": await ManageUsersAsync(); break;
                case "0" : Console.Clear(); Console.WriteLine("Exiting..."); break;
                default : Console.Clear(); Console.WriteLine("Invalid input."); break;
            }
        }
    }

    private async Task ManageUsersAsync()
    {
        var manageUsersView = new ManageUsersView(_userRepository,
            _postRepository, _commentRepository);
        await manageUsersView.displayMenu();
    }

    private async Task ManagePostsAsync()
    {
        var managePostsView = new ManagePostsView(_userRepository,
            _postRepository, _commentRepository);
        await managePostsView.displayMenu();
    }

    private async Task ManageCommentsAsync()
    {
        ManageCommentsView manageCommentsView = new ManageCommentsView(_commentRepository);
        await manageCommentsView.ShowAsync();
    }
}