
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsReferences.Models;

namespace Cut_Roll_News.Core.NewsReferences.Repositories;

public interface INewsReferenceRepository : ICreateAsync<NewsReference, Guid?>
{
    Task<IEnumerable<NewsReference>> GetAllByArticleIdAsync(Guid articleId);
    Task<int> GetCountByArticleIdAsync(Guid articleId);
    Task<bool> ExistsAsync(Guid referencedId, Guid articleId);
    Task<Guid?> DeleteByArticleIdAndReferenceIdAsync(Guid articleId, Guid referencedId);
}
