namespace DTOs.Comment;

public class CommentDTO
{
    public int Id { get; set; }
<<<<<<< Updated upstream
    public int PostId { get; set; }
    public string Body { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
=======
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    public CommentDTO()
    {
        
    }
    public CommentDTO(string body, int postID, int userID)
    {
        Body = body;
        PostId = postID;
        UserId = userID;
    }
>>>>>>> Stashed changes
}