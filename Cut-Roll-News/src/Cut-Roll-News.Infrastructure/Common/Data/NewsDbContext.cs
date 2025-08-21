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

    public DbSet<ExecutedScript> ExecutedScripts { get; set; } = default!;
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Cast> Cast { get; set; }
    public DbSet<Crew> Crew { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<MovieProductionCompany> MovieProductionCompanies { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<MovieProductionCountry> MovieProductionCountries { get; set; }
    public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
    public DbSet<MovieSpokenLanguage> MovieSpokenLanguages { get; set; }
    public DbSet<MovieVideo> Videos { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<MovieKeyword> MovieKeywords { get; set; }
    public DbSet<MovieOriginCountry> MovieOriginCountries { get; set; }
    public DbSet<MovieImage> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsArticleConfiguration());
        modelBuilder.ApplyConfiguration(new NewsReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new NewsLikeConfiguration());
    }
}
