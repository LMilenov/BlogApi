using Blog.API.Data;
using Blog.API.DTOs;
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
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PostReadDto>>> GetPosts(int id)
    {
        var posts = await _context.Posts
        .Include(p => p.Comments)
        .Include(p => p.PostTags)
        .ThenInclude(pt => pt.Tag)
        .Where(p => p.Id == id)
        .Select(p => new PostReadDto
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            CreatedAtUtc = p.CreatedAtUtc,
            Tags = p.PostTags.Select(pt => pt.Tag!.Name).ToList(),
            Comments = p.Comments.Select(c => new CommentReadDto
            {
                Id = c.Id,
                Author = c.Author,
                Body = c.Body,
                CreatedAtUtc = c.CreatedAtUtc
            }).ToList()
        })
        .FirstOrDefaultAsync();

        if (posts == null)
        {
            return NotFound();
        }

        return Ok(posts);
    }

    [HttpPost]
    public async Task<ActionResult<PostReadDto>> CreatePost(PostCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest("Title is required");
        }

        var post = new Post
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        if (dto.TagIds != null && dto.TagIds.Any())
        {
            foreach (var tagId in dto.TagIds)
            {
                var tagExists = await _context.Tags.AnyAsync(t => t.Id == tagId);
                if (!tagExists)
                {
                    return BadRequest("Invalid tagId: {tagId}");
                }
                _context.PostTags.Add(new PostTag { PostId = post.Id, TagId = tagId });
            }
            await _context.SaveChangesAsync();
        }

        var result = new PostReadDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreatedAtUtc = post.CreatedAtUtc,
            Tags = _context.PostTags
            .Where(pt => pt.PostId == post.Id)
            .Select(pt => pt.Tag!.Name)
            .ToList(),
            Comments = new List<CommentReadDto>()
        };

        return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, result);
    }
}