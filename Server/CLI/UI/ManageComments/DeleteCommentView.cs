﻿using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class DeleteCommentView
{
    private readonly ICommentRepository commentRepository; 
            
    public DeleteCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task DeleteComment()
    {
        Console.Write("Enter the ID of the comment to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int commentId))
        {
            Console.WriteLine("Invalid comment ID.");
            return;
        }

        var comment = await commentRepository.GetSingleAsync(commentId);
        if (comment == null)
        {
            Console.WriteLine($"Comment with ID {commentId} not found.");
            return;
        }

        await commentRepository.DeleteAsync(commentId);
        Console.WriteLine($"Comment with ID {commentId} deleted successfully.");
    }
}