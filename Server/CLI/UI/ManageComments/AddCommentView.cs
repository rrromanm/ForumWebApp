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
    
    public async Task addCommentAsync(string body, int userId, int postID)
    {
        Comment comment = new Comment(body, userId, postID);
        await _commentRepository.AddAsync(comment);
        Console.WriteLine($"Comment created successfully on post: {postID} by {userId}.");
    }
}