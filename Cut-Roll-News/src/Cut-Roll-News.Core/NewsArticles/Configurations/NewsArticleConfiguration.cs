using Cut_Roll_News.Core.NewsArticles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_News.Core.NewsArticles.Configurations;

public class NewsArticleConfiguration : IEntityTypeConfiguration<NewsArticle>
{
    public void Configure(EntityTypeBuilder<NewsArticle> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(n => n.AuthorId)
            .IsRequired();

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Content)
            .IsRequired();

        builder.HasMany(n => n.NewsReferences)
            .WithOne()
            .HasForeignKey(r => r.NewsArticleId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(n => n.NewsLikes)
            .WithOne()
            .HasForeignKey(l => l.NewsArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}



