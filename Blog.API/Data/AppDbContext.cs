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

        //Seed Tags
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "C#", },
            new Tag { Id = 2, Name = "ASP.NET", },
            new Tag { Id = 3, Name = "PostgreSQL" }
        );

        //Seed Posts
        modelBuilder.Entity<Post>().HasData(
            new Post { Id = 1, Title = "Hello world", Content = "This is my first blog post!", CreatedAtUtc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)},
            new Post { Id = 2, Title = "Learning ASP>NET Core", Content = "ASP.NET Core makes building APIs easy!", CreatedAtUtc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)}
        );

        //Seed PostTags
        modelBuilder.Entity<PostTag>().HasData(
            new PostTag {PostId = 1, TagId = 1 },
            new PostTag { PostId = 2, TagId = 1},
            new PostTag { PostId = 2, TagId = 2}
        );

        //Seed Comments
        modelBuilder.Entity<Comment>().HasData(
            new Comment { Id = 1, PostId = 1, Author = "Lucy", Body = "Nice post!", CreatedAtUtc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)},
            new Comment{ Id = 2, PostId = 2, Author = "Darin", Body = "I love ASP.NET", CreatedAtUtc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)}
        );

        modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });

        modelBuilder.Entity<PostTag>().HasOne(pt => pt.Post).WithMany(p => p.PostTags).HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>().HasOne(pt => pt.Tag).WithMany(t => t.PostTags).HasForeignKey(pt => pt.TagId);

        modelBuilder.Entity<Post>().HasIndex(p => p.CreatedAtUtc);

        modelBuilder.Entity<Tag>().HasIndex(t => t.Name).IsUnique(false);
    }
}