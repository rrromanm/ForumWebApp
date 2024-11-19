using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Comment
{
    [Key] public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    // Navigation Properties
    [ForeignKey("PostId")] public Post Post { get; set; }
    [ForeignKey("UserId")] public User User { get; set; }

    public Comment()
    {
    }

    public Comment(string body, int postID, int userID)
    {
        Body = body;
        PostId = postID;
        UserId = userID;
    }
}