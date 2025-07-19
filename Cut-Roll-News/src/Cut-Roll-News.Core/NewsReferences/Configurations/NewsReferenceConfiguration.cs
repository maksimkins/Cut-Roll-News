using Cut_Roll_News.Core.NewsReferences.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_News.Core.NewsReferences.Configurations;

public class NewsReferenceConfiguration : IEntityTypeConfiguration<NewsReference>
{
    public void Configure(EntityTypeBuilder<NewsReference> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.NewsArticleId)
            .IsRequired();

        builder.Property(n => n.ReferenceId)
            .IsRequired();

        builder.Property(n => n.ReferenceUrl)
            .IsRequired(false);

        builder.Property(n => n.ReferenceType)
            .IsRequired()
            .HasConversion<string>();

        builder.HasIndex(n => new { n.NewsArticleId, n.ReferenceId, n.ReferenceType })
            .IsUnique();
    }
}
