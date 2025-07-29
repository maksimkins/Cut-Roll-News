using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Models;

namespace Cut_Roll_News.Core.NewsLikes.Repositories;

public interface INewsLikeRepository : ICreateAsync<NewsLike, Guid?>
{
    public Task<Guid?> DeleteByUserIdAndArticleId(string userId, Guid articleId);
    public Task<IEnumerable<NewsArticle>> GetLikedNewsByUserIdAsync(string userId);
    public Task<NewsLike?> GetByUserIdAndArticleId(string userId, Guid articleId);
    public Task<int> GetLikesCountByArticleIdAsync(Guid articleId);
    public Task<bool> ExistsAsync(string userId, Guid articleId);
}
