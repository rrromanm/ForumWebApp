using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly ManagePostsView managePostsView;
    private readonly SinglePostView singlePostView;
    
    private readonly AddCommentView addCommentView;
    private readonly ListCommentsView listCommentsView;
    private readonly ManageCommentsView manageCommentsView;
    private readonly SingleCommentView singleCommentView;
    
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;
    private readonly ManageUsersView manageUsersView;
    private readonly SingleUserView singleUserView;
    

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        createPostView = new CreatePostView(postRepository);
        listPostsView = new ListPostsView(postRepository);
        managePostsView = new ManagePostsView(postRepository);
        singlePostView = new SinglePostView(postRepository, commentRepository);
        
        addCommentView = new AddCommentView(commentRepository);
        listCommentsView = new ListCommentsView(commentRepository);
        manageCommentsView = new ManageCommentsView(commentRepository);
        singleCommentView = new SingleCommentView(commentRepository);

        createUserView = new CreateUserView(userRepository);
        listUsersView = new ListUsersView(userRepository);
        manageUsersView = new ManageUsersView(userRepository);
        singleUserView = new SingleUserView(userRepository);
    }
    public async Task StartAsync()
    {
        var exit = false;

        while (!exit)
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
                case "1": ManagePosts(); break;
                case "2": ManageComments(); break;
                case "3": ManageUsers(); break;
                case "0": exit = true; break;
            }
        }
    }
    private async Task ManagePosts()
    {
        Console.Clear();
        Console.WriteLine("Manage posts: ");
        Console.WriteLine("1. Create Post");
        Console.WriteLine("2. Update Post");
        Console.WriteLine("3. Delete Post");
        Console.WriteLine("4. View all posts");
        Console.WriteLine("5. View single post");
        Console.Write("Select an option: ");
        var input = Console.ReadLine();

        switch (input)
        {
            case "1" :
                Console.Clear();
                Console.WriteLine("Enter title: ");
                var title = Console.ReadLine();
                Console.WriteLine("Enter content: ");
                var content = Console.ReadLine();
                await createPostView.addPostAsync(title, content);
                break;
            
            case "2" :
                Console.Clear();
                Console.WriteLine("Enter post ID which you would like to update: ");
                if (int.TryParse(Console.ReadLine(), out int postIdToUpdate))
                {
                    Console.Write("Enter New Title: ");
                    var newTitle = Console.ReadLine();
                    Console.Write("Enter New Content: ");
                    var newBody = Console.ReadLine();
                    await managePostsView.UpdatePostAsync(newTitle, newBody, postIdToUpdate);
                }
                break;
            
            case "3" :
                Console.Clear();
                Console.WriteLine("Enter post ID which you would like to delete: ");
                if (int.TryParse(Console.ReadLine(), out int postIdToDelete))
                {
                    await managePostsView.DeletePostAsync(postIdToDelete);
                }
                break;
                
            case "4":
                await listPostsView.DisplayPosts();
                break;
            
            case "5":
                Console.Clear();
                Console.WriteLine("Enter post ID which you would like to view: ");
                if (int.TryParse(Console.ReadLine(), out int postIdToView))
                {
                    await singlePostView.DisplayPostAsync(postIdToView);
                }
                break;
        }
    }

    private async Task ManageComments()
    {
        Console.Clear();
        Console.WriteLine("Manage Comments: ");
        Console.WriteLine("1. Create Comment");
        Console.WriteLine("2. Update Comment");
        Console.WriteLine("3. Delete Comment");
        Console.WriteLine("4. View All Comments");
        Console.WriteLine("5. View Single Comment");
        Console.WriteLine("6. View Comments by Post ID");
        
        Console.Write("Choose an option: ");
        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Write("Enter Comment Content: ");
                var body = Console.ReadLine();
                Console.Write("Enter User ID: ");
                if (int.TryParse(Console.ReadLine(), out int userID))
                {
                    Console.Write("Enter Post ID: ");
                    if (int.TryParse(Console.ReadLine(), out int postId))
                    {
                        await addCommentView.addCommentAsync(body, userID, postId);
                    }
                }
                break;
            
            case "2":
                Console.Write("Enter Comment ID to Update: ");
                if (int.TryParse(Console.ReadLine(), out int commentID))
                {
                    Console.Write("Enter New Comment Body: ");
                    var newContent = Console.ReadLine();
                    Console.Write("Enter User ID: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        await manageCommentsView.UpdateComment(commentID, newContent, userId, 0);
                    }
                }
                break;
            
            case "3":
                Console.Write("Enter Comment ID to Delete: ");
                if (int.TryParse(Console.ReadLine(), out int commentId))
                {
                    await manageCommentsView.DeleteComment(commentId);
                }
                break;
            
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            
            case "4":
                await listCommentsView.DisplayCommentsAsync();
                break;
            
            case "5":
                Console.Write("Enter Comment ID: ");
                if (int.TryParse(Console.ReadLine(), out int SinglePostID))
                {
                    await singleCommentView.DisplayComment(SinglePostID);
                }

                break;
            
            case "6":
                Console.Write("Enter Post ID to View Comments: ");
                if (int.TryParse(Console.ReadLine(), out int postID))
                {
                    await listCommentsView.DisplayCommentsByIdAsync(postID);
                }
                break;
        }
    }

    private async Task ManageUsers()
    {
        Console.Clear();
        Console.WriteLine("Manage users: ");
        Console.WriteLine("1. Create an user");
        Console.WriteLine("2. Update an user");
        Console.WriteLine("3. Delete an user");
        Console.WriteLine("4. View all users");
        Console.WriteLine("5. View single user");
        Console.Write("Select an option: ");
        var input = Console.ReadLine();

        switch (input)
        {
            case "1" :
                Console.Clear();
                Console.WriteLine("Enter username: ");
                var username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                var password = Console.ReadLine();
                await createUserView.addUserAsync(username, password);
                break;
            
            case "2" :
                Console.Clear();
                Console.WriteLine("Enter username ID which you would like to update: ");
                if (int.TryParse(Console.ReadLine(), out int userIdToUpdate))
                {
                    Console.Write("Enter New Username: ");
                    var newUsername = Console.ReadLine();
                    Console.Write("Enter New Password: ");
                    var newPassword = Console.ReadLine();
                    await manageUsersView.UpdateUserAsync(newUsername, newPassword, userIdToUpdate);
                }
                break;
            
            case "3" :
                Console.Clear();
                Console.WriteLine("Enter username ID which you would like to delete: ");
                if (int.TryParse(Console.ReadLine(), out int userIdToDelete))
                {
                    await manageUsersView.DeleteUserAsync(userIdToDelete);
                }
                break;
            
            case "4":
                await listUsersView.DisplayUsers();
                break;
                
            case "5" :
                Console.Clear();
                Console.WriteLine("Enter username ID which you would like to view: ");
                if (int.TryParse(Console.ReadLine(), out int userIdToView))
                {
                    await singleUserView.DisplayUserAsync(userIdToView);
                }
                break;
        }
    }
}