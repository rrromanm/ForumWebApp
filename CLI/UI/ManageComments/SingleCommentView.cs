using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class SingleCommentView
{
    private readonly ICommentRepository _commentRepository;

    public SingleCommentView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task DisplayComment(int commentID)
    {
        Comment comment = await _commentRepository.GetSingleAsync(commentID);
        Console.WriteLine($"Comment ID: {comment.Id}, Post ID: {comment.PostId}, " +
                          $"User ID: {comment.UserId}, Content: {comment.Body}, ");
    }
}