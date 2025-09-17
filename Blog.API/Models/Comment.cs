namespace Blog.API.Models;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
    public string AuthorName { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}