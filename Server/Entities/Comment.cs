namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    public Comment()
    {
        
    }
    public Comment(string body, int postID, int userID)
    {
        Body = body;
        PostId = postID;
        UserId = userID;
    }
    
    public Comment(string body, int postID, int userID, int id)
    {
        Id = id;
        Body = body;
        PostId = postID;
        UserId = userID;
    }
}