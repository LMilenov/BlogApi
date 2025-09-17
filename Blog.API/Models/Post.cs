using Microsoft.EntityFrameworkCore;

namespace Blog.API.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAtUtc { get; set; }


    public List<Comment> Comments { get; set; } = new();
    public List<PostTag> PostTags { get; set; } = new();
}