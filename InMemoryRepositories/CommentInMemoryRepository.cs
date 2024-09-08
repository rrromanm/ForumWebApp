using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>
        {
            new Comment { Id = 1, Body = "This is the first comment."},
            new Comment { Id = 2, Body = "This is the second comment."},
            new Comment { Id = 3, Body = "This is a comment on the second post."}
        };
    }
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Last().Id + 1 : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {comment.Id} not found.");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID {id} not found.");
        }
        comments.Remove(commentToRemove);
        
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    { 
        Comment? commentToGet = comments.SingleOrDefault(p => p.Id == id);
        if (commentToGet is null)
        {
            throw new InvalidOperationException($"Comment with ID {id} not found.");
        }
        return Task.FromResult(commentToGet);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}