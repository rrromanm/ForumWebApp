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

    public async Task addPostAsync()
    {
        Console.Clear();
        Console.WriteLine("Enter title: ");
        var title = Console.ReadLine();
        Console.WriteLine("Enter content: ");
        var content = Console.ReadLine();
        
        var newPost = new Post(title,content);
        await _postRepository.AddAsync(newPost);
        
        Console.WriteLine($"Post with Id: {newPost.Id} has been created !");
        
    }
}