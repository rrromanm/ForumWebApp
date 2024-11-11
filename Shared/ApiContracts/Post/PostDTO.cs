<<<<<<< Updated upstream
﻿public class PostDTO
=======
﻿using DTOs.Comment;

namespace DTOs.Post;

public class PostDTO
>>>>>>> Stashed changes
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
<<<<<<< Updated upstream
    public string Author { get; set; }
    public int UserId { get; set; }
=======
    public int UserId { get; set; }
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
>>>>>>> Stashed changes
}