using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsArticles.Services;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Core.NewsLikes.Services;
using Cut_Roll_News.Core.Users.Services;

namespace Cut_Roll_News.Infrastructure.NewsLikes.Services;

public class NewsLikeService : INewsLikeService
{
    private readonly INewsLikeRepository _newsLikeRepository;
    private readonly INewsArticleService _newsArticleService;
    private readonly IUserService _userService;
    public NewsLikeService(INewsLikeRepository newsLikeRepository, IUserService userService, INewsArticleService newsArticleService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _newsArticleService = newsArticleService ?? throw new ArgumentNullException(nameof(newsArticleService));
        _newsLikeRepository = newsLikeRepository ?? throw new ArgumentNullException(nameof(newsLikeRepository));
    }

    public async Task<Guid> CreateLikeAsync(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            throw new ArgumentException($"there is no user with id: {userId}");
            
        var news = await _newsArticleService.GetArticleAsNoTrackingAsync(articleId);
        if (news == null)
            throw new ArgumentException($"there is no user with id: {articleId}");

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
        
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            throw new ArgumentException($"there is no user with id: {userId}");
            
        var news = await _newsArticleService.GetArticleAsNoTrackingAsync(articleId);
        if (news == null)
            throw new ArgumentException($"there is no user with id: {articleId}");

        var like = await _newsLikeRepository.GetByUserIdAndArticleId(userId, articleId.Value);
        if (like == null)
            throw new ArgumentException($"there is no like to delete");

        return await _newsLikeRepository.DeleteByUserIdAndArticleId(userId, articleId.Value)
                    ?? throw new InvalidOperationException($"Failed to delete like for user: {userId} and article: {articleId}");
    }
    public async Task<NewsLike?> GetLikeByUserIdAndArticleId(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            throw new ArgumentException($"there is no user with id: {userId}");
            
        var news = await _newsArticleService.GetArticleAsNoTrackingAsync(articleId);
        if (news == null)
            throw new ArgumentException($"there is no user with id: {articleId}");
            
        return await _newsLikeRepository.GetByUserIdAndArticleId(userId, articleId.Value);
    }

    public async Task<PagedResult<NewsArticle>> GetLikedNewsByUserIdAsync(string? userId, int page, int pageSize)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            throw new ArgumentException($"there is no user with id: {userId}");
            

        return await _newsLikeRepository.GetLikedNewsByUserIdAsync(userId, page, pageSize);
    }

    public async Task<int> GetLikesCountByArticleIdAsync(Guid? articleId)
    {
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));
            
        var news = await _newsArticleService.GetArticleAsNoTrackingAsync(articleId);
        if (news == null)
            throw new ArgumentException($"there is no user with id: {articleId}");

        return await _newsLikeRepository.GetLikesCountByArticleIdAsync(articleId.Value);
    }

    public async Task<bool> IsArticleLikedByUserAsync(string? userId, Guid? articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            throw new ArgumentException($"there is no user with id: {userId}");
            
        var news = await _newsArticleService.GetArticleAsNoTrackingAsync(articleId);
        if (news == null)
            throw new ArgumentException($"there is no user with id: {articleId}");

        return await _newsLikeRepository.ExistsAsync(userId, articleId.Value);
    }
}
