namespace Cut_Roll_News.Infrastructure.Common.Data;

using Cut_Roll_News.Core.Casts.Configuration;
using Cut_Roll_News.Core.Casts.Models;
using Cut_Roll_News.Core.Common.Models;
using Cut_Roll_News.Core.Countries.Configurations;
using Cut_Roll_News.Core.Countries.Models;
using Cut_Roll_News.Core.Crews.Configurations;
using Cut_Roll_News.Core.Crews.Models;
using Cut_Roll_News.Core.Genres.Configurations;
using Cut_Roll_News.Core.Genres.Models;
using Cut_Roll_News.Core.Keywords.Configurations;
using Cut_Roll_News.Core.Keywords.Models;
using Cut_Roll_News.Core.MovieGenres.Configurations;
using Cut_Roll_News.Core.MovieGenres.Models;
using Cut_Roll_News.Core.MovieImages.Configurations;
using Cut_Roll_News.Core.MovieImages.Models;
using Cut_Roll_News.Core.MovieKeywords.Configurations;
using Cut_Roll_News.Core.MovieKeywords.Models;
using Cut_Roll_News.Core.MovieOriginCountries.Configurations;
using Cut_Roll_News.Core.MovieOriginCountries.Models;
using Cut_Roll_News.Core.MovieProductionCompanies.Configurations;
using Cut_Roll_News.Core.MovieProductionCompanies.Models;
using Cut_Roll_News.Core.MovieProductionCountries.Models;
using Cut_Roll_News.Core.Movies.Configurations;
using Cut_Roll_News.Core.Movies.Models;
using Cut_Roll_News.Core.MovieSpokenLanguages.Configurations;
using Cut_Roll_News.Core.MovieSpokenLanguages.Models;
using Cut_Roll_News.Core.MovieVideos.Configurations;
using Cut_Roll_News.Core.MovieVideos.Models;
using Cut_Roll_News.Core.NewsArticles.Configurations;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Configurations;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsReferences.Configurations;
using Cut_Roll_News.Core.NewsReferences.Models;
using Cut_Roll_News.Core.People.Configurations;
using Cut_Roll_News.Core.People.Models;
using Cut_Roll_News.Core.ProductionCompanies.Configurations;
using Cut_Roll_News.Core.ProductionCompanies.Models;
using Cut_Roll_News.Core.SpokenLanguages.Configurations;
using Cut_Roll_News.Core.SpokenLanguages.Models;
using Cut_Roll_News.Core.Users.Configurations;
using Cut_Roll_News.Core.Users.Models;
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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsArticleConfiguration());
        modelBuilder.ApplyConfiguration(new NewsReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new NewsLikeConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.Entity<ExecutedScript>().HasKey(e => e.ScriptName);

        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CastConfiguration());
        modelBuilder.ApplyConfiguration(new ProductionCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new CrewConfiguration());
        modelBuilder.ApplyConfiguration(new MovieProductionCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new MovieProductionCountryConfiguration());
        modelBuilder.ApplyConfiguration(new SpokenLanguageConfiguration());
        modelBuilder.ApplyConfiguration(new MovieSpokenLanguageConfiguration());
        modelBuilder.ApplyConfiguration(new MovieVideoConfiguration());
        modelBuilder.ApplyConfiguration(new KeywordConfiguration());
        modelBuilder.ApplyConfiguration(new MovieKeywordConfiguration());
        modelBuilder.ApplyConfiguration(new MovieOriginCountryConfiguration());
        modelBuilder.ApplyConfiguration(new MovieImageConfiguration());
    }
}
