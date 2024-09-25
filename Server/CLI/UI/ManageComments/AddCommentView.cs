using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class AddCommentView
{
    private readonly ICommentRepository _commentRepository;

    public AddCommentView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task addCommentAsync()
    {
        Console.Write("Enter Comment Content: ");
        var body = Console.ReadLine();
        
        Console.WriteLine("Enter the post ID you want to comment on:");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        Console.Write("Enter user ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        var comment = new Comment(body, postId, userId);
        var createdComment = await _commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment added successfully with ID: {createdComment.Id}");
    }
}