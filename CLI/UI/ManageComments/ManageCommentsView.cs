using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository _commentRepository;
    public ManageCommentsView(ICommentRepository commentRepository)
    {
        this._commentRepository = commentRepository;
    }

    public async Task DeleteComment(int commentID)
    {
        await _commentRepository.DeleteAsync(commentID);
        Console.WriteLine($"Comment with ID {commentID} has been deleted.");
    }

    public async Task UpdateComment(int commentID, string newContent, int userID, int postID)
    {
        Comment comment = new Comment(newContent, postID, userID, postID);    
        await _commentRepository.UpdateAsync(comment);
        Console.WriteLine($"Comment with ID {commentID} has been updated.");
    }
}