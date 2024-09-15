using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository _commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task DisplayCommentsByIdAsync(int postID)
    {
        var comments = await _commentRepository.GetCommentsByPostIdAsync(postID);
    
        Console.WriteLine($"Listing Comments for post {postID}:");
        
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"- {comment.Id}: {comment.Body}");
        }
    }

    public async Task DisplayCommentsAsync()
    {
        Console.WriteLine("Listing all comments:");
        foreach (Comment comment in _commentRepository.GetMany())
        {
            Console.WriteLine($"- {comment.Id}: {comment.Body}");
        }
    }
}