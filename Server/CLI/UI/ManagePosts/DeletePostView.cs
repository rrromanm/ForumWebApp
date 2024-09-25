using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository _postRepository;

    public DeletePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }   
    public async Task DeletePostAsync()
    {
        Console.Clear();
        Console.WriteLine("Enter post ID which you would like to delete: ");
        if (int.TryParse(Console.ReadLine(), out int postIdToDelete))
        {
            Console.Clear();
            await _postRepository.DeleteAsync(postIdToDelete);
            Console.WriteLine($"Post with id {postIdToDelete} was deleted.");
        }
    }
}