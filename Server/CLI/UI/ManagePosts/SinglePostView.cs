using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }
    
    public async Task DisplayPostAsync()
    { 
        Console.Clear();
        Console.WriteLine("Enter post ID which you would like to view: ");
        if (int.TryParse(Console.ReadLine(), out int postIdToView))
        {
            Post? post = await _postRepository.GetSingleAsync(postIdToView);
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Content: {post.Body}");
        }
    }
}