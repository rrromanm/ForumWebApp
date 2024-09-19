namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

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