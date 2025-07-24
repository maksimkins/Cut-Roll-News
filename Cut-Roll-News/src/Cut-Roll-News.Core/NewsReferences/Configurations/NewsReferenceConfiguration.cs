using Cut_Roll_News.Core.NewsReferences.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_News.Core.NewsReferences.Configurations;

public class NewsReferenceConfiguration : IEntityTypeConfiguration<NewsReference>
{
    public void Configure(EntityTypeBuilder<NewsReference> builder)
    {
        builder.HasKey(n => new { n.NewsArticleId, n.ReferencedId });

        builder.Property(n => n.ReferenceType)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(nr => nr.NewsArticle)
            .WithMany(na => na.NewsReferences)
            .HasForeignKey(nr => nr.NewsArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}