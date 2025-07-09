namespace Cut_Roll_News.Infrastructure.Common.Data;

using Cut_Roll_News.Core.NewsArticles.Configurations;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Configurations;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsReferences.Configurations;
using Cut_Roll_News.Core.NewsReferences.Models;
using Microsoft.EntityFrameworkCore;

public class NewsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<NewsArticle> NewsArticles { get; set; }
    public DbSet<NewsReference> NewsReferences { get; set; }
    public DbSet<NewsLike> NewsLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsArticleConfiguration());
        modelBuilder.ApplyConfiguration(new NewsReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new NewsLikeConfiguration());
    }
}
