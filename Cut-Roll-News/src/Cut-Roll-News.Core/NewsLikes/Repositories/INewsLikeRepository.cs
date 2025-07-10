using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Models;

namespace Cut_Roll_News.Core.NewsLikes.Repositories;

public interface INewsLikeRepository : ICreateAsync<NewsLike, string?>,
    IDeleteByIdAsync<string, string?>,
    IGetAsNoTrackingAsync<NewsLike?, string>
{
    public Task<string?> DeleteByUserIdAndArticleId(string userId, string articleId);
    public Task<IQueryable<NewsArticle>> GetLikedNewsByUserIdAsync(string userId);
    public Task<NewsLike?> GetByUserIdAndArticleId(string userId, string articleId);
    public Task<int> GetLikesCountByArticleIdAsync(string articleId);
    public Task<bool> IsArticleLikedByUserAsync(string userId, string articleId);
}
