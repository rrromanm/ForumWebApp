using System.ComponentModel.DataAnnotations;

namespace Entities;

public class User
{
    [Key] public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    // Navigation Property
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public User()
    {
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(string username, string password, int id)
    {
        Username = username;
        Password = password;
        Id = id;
    }
}