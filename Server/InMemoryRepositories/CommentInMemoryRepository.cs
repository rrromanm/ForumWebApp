using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    List<Comment> comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        _ = AddAsync(new Comment("Hello World.", 1, 1)).Result;
        _ = AddAsync(new Comment("Good Post.", 3, 2)).Result;
        _ = AddAsync(new Comment("I don't like it.", 2, 3)).Result;
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
    
    public Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        var filteredComments = comments.Where(c => c.PostId == postId).ToList();
        return Task.FromResult(filteredComments);
    }
}