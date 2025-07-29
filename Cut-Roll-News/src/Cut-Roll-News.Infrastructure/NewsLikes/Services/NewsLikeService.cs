using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Core.NewsLikes.Services;

namespace Cut_Roll_News.Infrastructure.NewsLikes.Services;

public class NewsLikeService : INewsLikeService
{
    private readonly INewsLikeRepository _newsLikeRepository;
    public NewsLikeService(INewsLikeRepository newsLikeRepository)
    {
        _newsLikeRepository = newsLikeRepository ?? throw new ArgumentNullException(nameof(newsLikeRepository));
    }

    public async Task<Guid> CreateLikeAsync(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        return await _newsLikeRepository.CreateAsync(new NewsLike
        {
            UserId = userId,
            NewsArticleId = articleId.Value
        }) ?? throw new InvalidOperationException("Failed to create like.");
    }

    public async Task<Guid> DeleteLikeByUserIdAndArticleIdAsync(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        return await _newsLikeRepository.DeleteByUserIdAndArticleId(userId, articleId.Value)
                ?? throw new InvalidOperationException($"Failed to delete like for user: {userId} and article: {articleId}");
    }
    public async Task<NewsLike?> GetLikeByUserIdAndArticleId(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));
            
        return await _newsLikeRepository.GetByUserIdAndArticleId(userId, articleId.Value);
    }

    public async Task<IEnumerable<NewsArticle>> GetLikedNewsByUserIdAsync(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        return await _newsLikeRepository.GetLikedNewsByUserIdAsync(userId);
    }

    public async Task<int> GetLikesCountByArticleIdAsync(Guid? articleId)
    {
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));
        
        return await _newsLikeRepository.GetLikesCountByArticleIdAsync(articleId.Value);
    }

    public async Task<bool> IsArticleLikedByUserAsync(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        return await _newsLikeRepository.ExistsAsync(userId, articleId.Value);
    }
}
