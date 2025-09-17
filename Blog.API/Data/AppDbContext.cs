using Microsoft.EntityFrameworkCore;
using Blog.API.Models;

namespace Blog.API.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<PostTag> PostTags => Set<PostTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });

        modelBuilder.Entity<PostTag>().HasOne(pt => pt.Post).WithMany(p => p.PostTags).HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>().HasOne(pt => pt.Tag).WithMany(t => t.PostTags).HasForeignKey(pt => pt.TagId);

        modelBuilder.Entity<Post>().HasIndex(p => p.CreatedAtUtc);

        modelBuilder.Entity<Tag>().HasIndex(t => t.Name).IsUnique(false);
    }
}