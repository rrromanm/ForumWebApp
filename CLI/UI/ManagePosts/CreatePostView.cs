using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task addPostAsync(string title, string content)
    {
        Post? post = new Post(title, content);
        await _postRepository.AddAsync(post);
        
        Console.WriteLine($"Post '{title}' has been created successfully.");
    }
}