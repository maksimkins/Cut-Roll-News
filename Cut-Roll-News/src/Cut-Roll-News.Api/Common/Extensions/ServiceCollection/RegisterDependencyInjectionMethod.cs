using Cut_Roll_News.Core.Common.Services;
using Cut_Roll_News.Core.NewsArticles.Repositories;
using Cut_Roll_News.Core.NewsArticles.Services;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Core.NewsLikes.Services;
using Cut_Roll_News.Core.NewsReferences.Repositories;
using Cut_Roll_News.Infrastructure.Common.Services;
using Cut_Roll_News.Infrastructure.NewsArticles.Repositories;
using Cut_Roll_News.Infrastructure.NewsArticles.Services;
using Cut_Roll_News.Infrastructure.NewsLikes.Repositories;
using Cut_Roll_News.Infrastructure.NewsLikes.Services;
using Cut_Roll_News.Infrastructure.NewsReferences.Repositories;

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

        serviceCollection.AddTransient<IMessageBrokerService, RabbitMqService>();

        //serviceCollection.AddHostedService<UserRabbitMqService>();
    } 
}