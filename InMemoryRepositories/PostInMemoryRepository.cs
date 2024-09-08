using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts;
    
    public PostInMemoryRepository()
    {
        posts = new List<Post>
        {
            new Post { Id = 1, Title = "First Post", Body = "This is the body of the first post." },
            new Post { Id = 2, Title = "Second Post", Body = "This is the body of the second post." },
            new Post { Id = 3, Title = "Third Post", Body = "This is the body of the third post." }
        };
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Last().Id + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID {post.Id} not found.");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID {id} not found.");
        }
        posts.Remove(postToRemove);
        
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? postToGet = posts.SingleOrDefault(p => p.Id == id);
        if (postToGet is null)
        {
            throw new InvalidOperationException($"Post with ID {id} not found.");
        }
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}