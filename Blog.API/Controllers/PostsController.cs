using Blog.API.Data;
using Blog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly BlogDbContext _context;

    public PostsController(BlogDbContext context)
    {
        _context = context;
    }

    //Get api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _context.Posts
        .Include(p => p.Comments)
        .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .ToListAsync();

        return Ok(posts);
    }
}