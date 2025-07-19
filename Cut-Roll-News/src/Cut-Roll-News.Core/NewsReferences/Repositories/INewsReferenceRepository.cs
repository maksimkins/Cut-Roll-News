
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsReferences.Models;

namespace Cut_Roll_News.Core.NewsReferences.Repositories;

public interface INewsReferenceRepository : ICreateAsync<NewsReference, string?>,
    IDeleteByIdAsync<string, string?>,
    IGetAsNoTrackingAsync<NewsReference?, string>,
    IGetByIdAsync<NewsReference?, string>
{
    Task<IEnumerable<NewsReference>> GetAllByArticleIdAsync(string articleId);
    Task<int> GetCountByArticleIdAsync(string articleId);
    Task<bool> IsReferenceExistsAsync(string referenceId, string articleId);
    Task<string?> DeleteByArticleIdAndReferenceIdAsync(string articleId, string referencedId);
}
