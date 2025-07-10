namespace Cut_Roll_News.Core.NewsArticles.Repositories;

using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsArticles.Models;

public interface INewsArticleRepository : ICreateAsync<NewsArticle, string?>, IDeleteByIdAsync<string, string?>,
    IGetAsNoTrackingAsync<NewsArticle?, string>, IUpdateAsync<NewsArticle, string?>
{
    public Task<IQueryable<NewsArticle>> GetAllAsQueryableAsync();
    public Task<int> IncrementLikes(string articleId);
    public Task<int> DecrementLikes(string articleId);
}
