using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.API.Data;
using Blog.API.DTOs;
using Blog.API.Models;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly BlogDbContext _context;

    public TagsController(BlogDbContext context)
    {
        _context = context;
    }

    // GET: api/tags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagReadDto>>> GetTags()
    {
        var tags = await _context.Tags
            .Select(t => new TagReadDto { Id = t.Id, Name = t.Name })
            .ToListAsync();

        return Ok(tags);
    }

    // GET: api/tags/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TagReadDto>> GetTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        return Ok(new TagReadDto { Id = tag.Id, Name = tag.Name });
    }

    // POST: api/tags
    [HttpPost]
    public async Task<ActionResult<TagReadDto>> CreateTag(TagCreateDto dto)
    {
        var tag = new Tag { Name = dto.Name };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        var result = new TagReadDto { Id = tag.Id, Name = tag.Name };
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, result);
    }

    // PUT: api/tags/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(int id, TagCreateDto dto)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        tag.Name = dto.Name;
        await _context.SaveChangesAsync();

        return Ok(new TagReadDto { Id = tag.Id, Name = tag.Name });
    }

    // DELETE: api/tags/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
