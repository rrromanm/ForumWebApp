using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        // Check if the PostId exists
        var postExists = await ctx.Posts.AnyAsync(p => p.Id == comment.PostId);
        if (!postExists)
        {
            throw new ArgumentException($"Post with ID {comment.PostId} not found.");
        }

        // Check if the UserId exists
        var userExists = await ctx.Users.AnyAsync(u => u.Id == comment.UserId);
        if (!userExists)
        {
            throw new ArgumentException($"User with ID {comment.UserId} not found.");
        }

        // Add the comment
        EntityEntry<Comment> entry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entry.Entity;
    }


    public async Task UpdateAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new ArgumentException($"Comment with ID {comment.Id} not found.");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (existing == null)
        {
            throw new ArgumentException($"Comment with ID {id} not found.");
        }

        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);

        return comment;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return ctx.Comments.AsQueryable();
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await ctx.Comments.Where(c => c.PostId == postId).ToListAsync();
    }
}