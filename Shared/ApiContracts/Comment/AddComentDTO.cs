﻿namespace DTOs.Comment;

public class AddComentDTO
{
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
}