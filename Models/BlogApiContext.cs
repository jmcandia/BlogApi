using Microsoft.EntityFrameworkCore;
using BlogApi.Models.Configurations;

namespace BlogApi.Models;

public class BlogApiContext : DbContext
{
    public BlogApiContext(DbContextOptions<BlogApiContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new PostConfiguration())
            .ApplyConfiguration(new CommentConfiguration());
    }
}