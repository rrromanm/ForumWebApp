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
    
    public async Task DisplayPostAsync(int postId)
    { 
        Post? post = await _postRepository.GetSingleAsync(postId);
        Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Content: {post.Body}");
    }
}