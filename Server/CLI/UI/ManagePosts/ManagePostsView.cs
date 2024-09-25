using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ManagePostsView(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        
    }

    public async Task displayMenu()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Manage posts: ");
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. Update Post");
            Console.WriteLine("3. Delete Post");
            Console.WriteLine("4. View all posts");
            Console.WriteLine("5. View single post");
            Console.WriteLine("0. Back");
            Console.Write("Select an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreatePostAsync();
                    break;

                case "2":
                    await UpdatePostAsync();
                    break;

                case "3":
                    await DeletePostAsync();
                    break;

                case "4":
                    await ListPostsAsync();
                    break;

                case "5":
                    await SinglePostAsync();
                    break;
                case "0" :
                    await MainMenuAsync();
                    break;
            }
        }
    }

    private async Task CreatePostAsync()
    {
        var createPostView = new CreatePostView(postRepository);
        await createPostView.addPostAsync();
    }

    private async Task UpdatePostAsync()
    {
        var updatePostView = new UpdatePostView(postRepository);
        await updatePostView.UpdatePostAsync();
    }

    private async Task DeletePostAsync()
    {
        var deletePostView = new DeletePostView(postRepository);
        await deletePostView.DeletePostAsync();
    }

    private async Task ListPostsAsync()
    {
        var listPostsView = new ListPostsView(postRepository);
        await listPostsView.DisplayPosts();
    }

    private async Task SinglePostAsync()
    {
        var singlePostView = new SinglePostView(postRepository, commentRepository);
        await singlePostView.DisplayPostAsync();
    }

    private async Task MainMenuAsync()
    {
        var mainMenu = new CliApp(userRepository, postRepository, commentRepository);
        await mainMenu.StartAsync();
    }
    
}