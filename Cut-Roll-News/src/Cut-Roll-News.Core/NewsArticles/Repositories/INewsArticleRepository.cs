namespace Cut_Roll_News.Core.NewsArticles.Repositories;

using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsArticles.Models;

public interface INewsArticleRepository : ICreateAsync<NewsArticle, Guid?>, IDeleteByIdAsync<Guid, Guid?>,
    IGetAsNoTrackingAsync<NewsArticle?, Guid>, IUpdateAsync<NewsArticle, Guid?>
{
    public Task<IQueryable<NewsArticle>> GetAllAsQueryableAsync();
    public Task<int> IncrementLikes(Guid articleId);
    public Task<int> DecrementLikes(Guid articleId);
}
