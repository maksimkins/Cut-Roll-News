using Cut_Roll_News.Core.NewsLikes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_News.Core.NewsLikes.Configurations;

public class NewsLikeConfiguration : IEntityTypeConfiguration<NewsLike>
{
    public void Configure(EntityTypeBuilder<NewsLike> builder)
    {
        builder.HasKey(n => new { n.UserId, n.NewsArticleId });

        builder.HasOne(nl => nl.NewsArticle)
            .WithMany(na => na.NewsLikes)
            .HasForeignKey(nl => nl.NewsArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
