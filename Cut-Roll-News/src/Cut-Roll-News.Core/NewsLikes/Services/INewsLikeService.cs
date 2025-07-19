using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Dtos;
using Cut_Roll_News.Core.NewsLikes.Models;

namespace Cut_Roll_News.Core.NewsLikes.Services;

public interface INewsLikeService
{
    public Task<string> CreateLikeAsync(NewsLikeCreateDto createDto);
    public Task<string> DeleteLikeByIdAsync(string likeId);
    public Task<string> DeleteLikeByUserIdAndArticleIdAsync(string userId, string articleId);
    public Task<NewsLike> GetLikeAsNoTrackingAsync(string likeId);
    public Task<IEnumerable<NewsArticle>> GetLikedNewsByUserIdAsync(string userId);
    public Task<NewsLike> GetLikeByUserIdAndArticleId(string userId, string articleId);
    public Task<int> GetLikesCountByArticleIdAsync(string articleId);
    public Task<bool> IsArticleLikedByUserAsync(string userId, string articleId);
}
