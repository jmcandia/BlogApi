using Microsoft.EntityFrameworkCore;
using BlogApi.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models;

public class BlogApiContext : IdentityUserContext<IdentityUser>
{
    public BlogApiContext(DbContextOptions<BlogApiContext> options)
        : base(options) { }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new PostConfiguration())
            .ApplyConfiguration(new CommentConfiguration());
    }
}