using DTOs.Comment;

public class PostWithCommentsDTO
{
    public PostDTO Post { get; set; }
    public List<CommentDTO> Comments { get; set; }
}