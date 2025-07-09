using Cut_Roll_News.Core.NewsLikes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_News.Core.NewsLikes.Configurations;

public class NewsLikeConfiguration : IEntityTypeConfiguration<NewsLike>
{
    public void Configure(EntityTypeBuilder<NewsLike> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.UserId)
            .IsRequired();

        builder.Property(n => n.NewsArticleId)
            .IsRequired();

        builder.HasIndex(n => new { n.UserId, n.NewsArticleId })
            .IsUnique();
    }
}
