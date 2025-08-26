using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Models;

namespace Cut_Roll_News.Core.NewsLikes.Services;

public interface INewsLikeService
{
    public Task<Guid> CreateLikeAsync(string? userId, Guid? articleId);
    public Task<Guid> DeleteLikeByUserIdAndArticleIdAsync(string? userId, Guid? articleId);
    public Task<PagedResult<NewsArticle>> GetLikedNewsByUserIdAsync(string? userId, int page, int pageSize);
    public Task<NewsLike?> GetLikeByUserIdAndArticleId(string? userId, Guid? articleId);
    public Task<int> GetLikesCountByArticleIdAsync(Guid? articleId);
    public Task<bool> IsArticleLikedByUserAsync(string? userId, Guid? articleId);
}
