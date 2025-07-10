
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsReferences.Models;

namespace Cut_Roll_News.Core.NewsReferences.Repositories;

public interface INewsReferenceRepository : ICreateAsync<NewsReference, string?>,
    IDeleteByIdAsync<string, string?>,
    IGetAsNoTrackingAsync<NewsReference?, string>,
    IUpdateAsync<NewsReference, string?>, IGetByIdAsync<string, NewsReference?>
{
    Task<IQueryable<NewsReference>> GetAllByArticleIdAsync(int articleId);
    Task<int> GetCountByArticleIdAsync(int articleId);
    Task<bool> IsReferenceExistsAsync(string referenceId, int articleId);
}
