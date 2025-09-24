using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.API.Data;
using Blog.API.DTOs;
using Blog.API.Models;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/posts/{postId}/comments")]
public class CommentsController : ControllerBase
{
    private readonly BlogDbContext _context;
    public CommentsController(BlogDbContext context)
    {
        _context = context;
    }

    // GET: api/posts/{postId}/comments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentReadDto>>> GetComments(int postId)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId);
        if (!postExists) return NotFound($"Post {postId} not found");

        var comments = await _context.Comments
            .Where(c => c.PostId == postId)
            .Select(c => new CommentReadDto
            {
                Id = c.Id,
                Author = c.Author,
                Body = c.Body,
                CreatedAtUtc = c.CreatedAtUtc
            })
            .ToListAsync();

        return Ok(comments);
    }

    // POST: api/posts/{postId}/comments
    [HttpPost]
    public async Task<ActionResult<CommentReadDto>> AddComment(int postId, CommentCreateDto dto)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId);
        if (!postExists) return NotFound($"Post {postId} not found");

        var comment = new Comment
        {
            PostId = postId,
            Author = dto.Author,
            Body = dto.Body,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var result = new CommentReadDto
        {
            Id = comment.Id,
            Author = comment.Author,
            Body = comment.Body,
            CreatedAtUtc = comment.CreatedAtUtc
        };

        return CreatedAtAction(nameof(GetComments), new { postId }, result);
    }
}
