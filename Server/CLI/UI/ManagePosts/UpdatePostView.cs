using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository _postRepository;

    public UpdatePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public async Task UpdatePostAsync()
    {
        Console.Clear();
        Console.WriteLine("Enter post ID which you would like to update: ");
        if (int.TryParse(Console.ReadLine(), out int postIdToUpdate))
        {
            Console.Write("Enter New Title: ");
            var newTitle = Console.ReadLine();
            Console.Write("Enter New Content: ");
            var newBody = Console.ReadLine();
            Post post = new Post(newTitle, newBody, postIdToUpdate);
            await _postRepository.UpdateAsync(post);
            Console.WriteLine($"Post with ID {postIdToUpdate} updated successfully.");
        }
    }
}