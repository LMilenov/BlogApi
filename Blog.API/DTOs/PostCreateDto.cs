namespace Blog.API.DTOs;

public class PostCreateDto
{
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public List<int> TagIds { get; set; } = new();
}