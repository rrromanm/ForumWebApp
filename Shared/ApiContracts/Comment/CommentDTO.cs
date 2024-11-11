namespace DTOs.Comment;

public class CommentDTO
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Body { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}