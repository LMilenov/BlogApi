namespace Blog.API.DTOs;

public class PostReadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }
    public List<string> Tags { get; set; } = new();

    public List<CommentReadDto> Comments { get; set; } = new();
}