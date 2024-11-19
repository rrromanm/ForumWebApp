using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Post
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }

    // Navigation Property
    [ForeignKey("UserId")] public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }

    public Post()
    {
    }

    public Post(string title, string body)
    {
        Title = title;
        Body = body;
    }

    public Post(string title, string body, int id)
    {
        Title = title;
        Body = body;
        Id = id;
    }
}