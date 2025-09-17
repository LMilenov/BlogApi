namespace Blog.API.DTOs;

public class CommentCreateDto
{
    public string Author { get; set; } = "";
    public string Content { get; set; } = "";
    public int PostId { get; set; }
}