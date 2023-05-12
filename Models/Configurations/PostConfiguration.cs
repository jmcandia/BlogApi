using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApi.Models.Configurations;
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Content).IsRequired().HasMaxLength(500);
        builder.Property(p => p.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.HasMany(p => p.Comments).WithOne(c => c.Post).OnDelete(DeleteBehavior.Restrict);
    }
}