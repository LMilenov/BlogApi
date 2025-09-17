namespace Blog.API.DTOs;

public class CommentReadDto
{
    public int Id { get; set; }
    public string Author { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}