using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository _postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task DisplayPosts()
    {
        Console.WriteLine("Listing posts...");
        foreach (Post post in _postRepository.GetMany())
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Content: {post.Body}");
        }
    }
}