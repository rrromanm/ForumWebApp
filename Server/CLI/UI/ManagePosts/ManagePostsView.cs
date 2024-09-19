using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository _postRepository;

    public ManagePostsView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public async Task UpdatePostAsync(string title, string content, int postId)
    {
        Post post = new Post(title, content, postId);
        await _postRepository.UpdateAsync(post);
        Console.WriteLine($"Post with ID {postId} updated successfully.");
    }

    public async Task DeletePostAsync(int postId)
    {
        await _postRepository.DeleteAsync(postId);
        Console.WriteLine($"Post with ID {postId} deleted successfully.");
    }
}