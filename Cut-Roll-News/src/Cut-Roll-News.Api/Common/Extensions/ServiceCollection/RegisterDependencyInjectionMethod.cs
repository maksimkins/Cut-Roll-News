using Cut_Roll_News.Core.Blob.Managers;
using Cut_Roll_News.Core.Casts.Repositories;
using Cut_Roll_News.Core.Casts.Services;
using Cut_Roll_News.Core.Common.Services;
using Cut_Roll_News.Core.Countries.Repositories;
using Cut_Roll_News.Core.Countries.Services;
using Cut_Roll_News.Core.Crews.Repositories;
using Cut_Roll_News.Core.Crews.Services;
using Cut_Roll_News.Core.Genres.Repositories;
using Cut_Roll_News.Core.Genres.Services;
using Cut_Roll_News.Core.Keywords.Repositories;
using Cut_Roll_News.Core.Keywords.Services;
using Cut_Roll_News.Core.MovieGenres.Repositories;
using Cut_Roll_News.Core.MovieGenres.Services;
using Cut_Roll_News.Core.MovieImages.Repositories;
using Cut_Roll_News.Core.MovieImages.Service;
using Cut_Roll_News.Core.MovieKeywords.Repositories;
using Cut_Roll_News.Core.MovieKeywords.Service;
using Cut_Roll_News.Core.MovieOriginCountries.Repository;
using Cut_Roll_News.Core.MovieOriginCountries.Service;
using Cut_Roll_News.Core.MovieProductionCompanies.Repositories;
using Cut_Roll_News.Core.MovieProductionCompanies.Service;
using Cut_Roll_News.Core.MovieProductionCountries.Repositories;
using Cut_Roll_News.Core.MovieProductionCountries.Service;
using Cut_Roll_News.Core.Movies.Repositories;
using Cut_Roll_News.Core.Movies.Service;
using Cut_Roll_News.Core.MovieSpokenLanguages.Repositories;
using Cut_Roll_News.Core.MovieSpokenLanguages.Service;
using Cut_Roll_News.Core.MovieVideos.Repositories;
using Cut_Roll_News.Core.MovieVideos.Service;
using Cut_Roll_News.Core.NewsArticles.Repositories;
using Cut_Roll_News.Core.NewsArticles.Services;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Core.NewsLikes.Services;
using Cut_Roll_News.Core.NewsReferences.Repositories;
using Cut_Roll_News.Core.People.Repositories;
using Cut_Roll_News.Core.People.Service;
using Cut_Roll_News.Core.ProductionCompanies.Repositores;
using Cut_Roll_News.Core.ProductionCompanies.Service;
using Cut_Roll_News.Core.SpokenLanguages.Repositories;
using Cut_Roll_News.Core.SpokenLanguages.Service;
using Cut_Roll_News.Core.Users.Repositories;
using Cut_Roll_News.Core.Users.Services;
using Cut_Roll_News.Infrastructure.Cast.Repositories;
using Cut_Roll_News.Infrastructure.Cast.Services;
using Cut_Roll_News.Infrastructure.Common.Services;
using Cut_Roll_News.Infrastructure.Countries.Repositories;
using Cut_Roll_News.Infrastructure.Countries.Services;
using Cut_Roll_News.Infrastructure.Crew.Repositories;
using Cut_Roll_News.Infrastructure.Crew.Services;
using Cut_Roll_News.Infrastructure.Genres.Repositories;
using Cut_Roll_News.Infrastructure.Genres.Services;
using Cut_Roll_News.Infrastructure.Keywords.Repositories;
using Cut_Roll_News.Infrastructure.Keywords.Services;
using Cut_Roll_News.Infrastructure.MovieGenres.Repositories;
using Cut_Roll_News.Infrastructure.MovieGenres.Services;
using Cut_Roll_News.Infrastructure.MovieImages.Repositories;
using Cut_Roll_News.Infrastructure.MovieImages.Services;
using Cut_Roll_News.Infrastructure.MovieKeywords.Repositories;
using Cut_Roll_News.Infrastructure.MovieKeywords.Services;
using Cut_Roll_News.Infrastructure.MovieOriginCountries.Repositories;
using Cut_Roll_News.Infrastructure.MovieOriginCountries.Services;
using Cut_Roll_News.Infrastructure.MovieProductionCompanies.Repositories;
using Cut_Roll_News.Infrastructure.MovieProductionCompanies.Services;
using Cut_Roll_News.Infrastructure.MovieProductionCountries.Repositories;
using Cut_Roll_News.Infrastructure.MovieProductionCountries.Services;
using Cut_Roll_News.Infrastructure.Movies.Repositories;
using Cut_Roll_News.Infrastructure.Movies.Services;
using Cut_Roll_News.Infrastructure.MovieSpokenLanguages.Repositories;
using Cut_Roll_News.Infrastructure.MovieSpokenLanguages.Services;
using Cut_Roll_News.Infrastructure.MovieVideos.Repositories;
using Cut_Roll_News.Infrastructure.MovieVideos.Services;
using Cut_Roll_News.Infrastructure.NewsArticles.Managers;
using Cut_Roll_News.Infrastructure.NewsArticles.Repositories;
using Cut_Roll_News.Infrastructure.NewsArticles.Services;
using Cut_Roll_News.Infrastructure.NewsLikes.Repositories;
using Cut_Roll_News.Infrastructure.NewsLikes.Services;
using Cut_Roll_News.Infrastructure.NewsReferences.Repositories;
using Cut_Roll_News.Infrastructure.People.Repositories;
using Cut_Roll_News.Infrastructure.People.Services;
using Cut_Roll_News.Infrastructure.ProductionCountries.Repositories;
using Cut_Roll_News.Infrastructure.ProductionCountries.Services;
using Cut_Roll_News.Infrastructure.SpokenLanguages.Repositories;
using Cut_Roll_News.Infrastructure.SpokenLanguages.Services;
using Cut_Roll_News.Infrastructure.Users.BackgroundServices;
using Cut_Roll_News.Infrastructure.Users.Repositories;
using Cut_Roll_News.Infrastructure.Users.Services;

namespace Cut_Roll_News.Api.Common.Extensions.ServiceCollection;

public static class RegisterDependencyInjectionMethod
{
    public static void RegisterDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<INewsArticleRepository, NewsArticleEfCoreRepository>();
        serviceCollection.AddTransient<INewsLikeRepository, NewsLikeEfCoreRepository>();
        serviceCollection.AddTransient<INewsReferenceRepository, NewsReferenceEfCoreRepository>();

        serviceCollection.AddTransient<INewsArticleService, NewsArticleService>();
        serviceCollection.AddTransient<INewsLikeService, NewsLikeService>();
        serviceCollection.AddTransient<IMovieRepository, MovieEfCoreRepository>();
        serviceCollection.AddTransient<ICastRepository, CastEfCoreRepository>();
        serviceCollection.AddTransient<ICrewRepository, CrewEfCoreRepository>();
        serviceCollection.AddTransient<IGenreRepository, GenreEfCoreRepository>();
        serviceCollection.AddTransient<IKeywordRepository, KeywordEfCoreRepository>();
        serviceCollection.AddTransient<IPersonRepository, PersonEfCoreRepository>();
        serviceCollection.AddTransient<ISpokenLanguageRepository, SpokenLanguageEfCoreRepository>();
        serviceCollection.AddTransient<IProductionCompanyRepository, ProductionCompanyEfCoreRepository>();
        serviceCollection.AddTransient<ICountryRepository, CountryEfCoreRepository>();
        serviceCollection.AddTransient<IMovieGenreRepository, MovieGenreEfCoreRepository>();
        serviceCollection.AddTransient<IMovieImageRepository, MovieImageEfCoreRepository>();
        serviceCollection.AddTransient<IMovieVideoRepository, MovieVideoEfCoreRepository>();
        serviceCollection.AddTransient<IMovieKeywordRepository, MovieKeywordEfCoreRepository>();
        serviceCollection.AddTransient<IMovieOriginCountryRepository, MovieOriginCountryEfCoreRepository>();
        serviceCollection.AddTransient<IMovieProductionCompanyRepository, MovieProductionCompanyEfCoreRepository>();
        serviceCollection.AddTransient<IMovieProductionCountryRepository, MovieProductionCountryEfCoreRepository>();
        serviceCollection.AddTransient<IMovieSpokenLanguageRepository, MovieSpokenLanguageEfCoreRepository>();
        

        serviceCollection.AddTransient<IMovieService, MovieService>();
        serviceCollection.AddTransient<ICastService, CastService>();
        serviceCollection.AddTransient<ICrewService, CrewService>();
        serviceCollection.AddTransient<IGenreService, GenreService>();
        serviceCollection.AddTransient<IKeywordService, KeywordService>();
        serviceCollection.AddTransient<IPersonService, PersonService>();
        serviceCollection.AddTransient<ISpokenLanguageService, SpokenLanguageService>();
        serviceCollection.AddTransient<IProductionCompanyService, ProductionCompanyService>();
        serviceCollection.AddTransient<ICountryService, CountryService>();
        serviceCollection.AddTransient<IMovieGenreService, MovieGenreService>();
        serviceCollection.AddTransient<IMovieImageService, MovieImageService>();
        serviceCollection.AddTransient<IMovieVideoService, MovieVideoService>();
        serviceCollection.AddTransient<IMovieKeywordService, MovieKeywordService>();
        serviceCollection.AddTransient<IMovieOriginCountryService, MovieOriginCountryService>();
        serviceCollection.AddTransient<IMovieProductionCompanyService, MovieProductionCompanyService>();
        serviceCollection.AddTransient<IMovieProductionCountryService, MovieProductionCountryService>();
        serviceCollection.AddTransient<IMovieSpokenLanguageService, MovieSpokenLanguageService>();

        serviceCollection.AddTransient<IUserRepository, UserEfCoreRepository>();
        serviceCollection.AddTransient<IUserService, UserService>();

        serviceCollection.AddTransient<IMessageBrokerService, RabbitMqService>();

        serviceCollection.AddTransient<BaseBlobImageManager<Guid>, NewsArticleImageManager>();

        serviceCollection.AddHostedService<UserRabbitMqService>();
    } 
}