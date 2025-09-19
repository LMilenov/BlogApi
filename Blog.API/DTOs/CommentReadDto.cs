namespace Blog.API.DTOs;

public class CommentReadDto
{
    public int Id { get; set; }
    public string Author { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }
}