using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsArticles.Repositories;
using Cut_Roll_News.Core.NewsLikes.Dtos;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Core.NewsLikes.Services;

namespace Cut_Roll_News.Infrastructure.NewsLikes.Services;

public class NewsLikeService : INewsLikeService
{
    private readonly INewsLikeRepository _newsLikeRepository;
    //private readonly INewsArticleRepository _newsArticleRepository;
    public NewsLikeService(INewsLikeRepository newsLikeRepository) // INewsArticleRepository newsArticleRepository
    {
        _newsLikeRepository = newsLikeRepository ?? throw new ArgumentNullException(nameof(newsLikeRepository));
        //_newsArticleRepository = newsArticleRepository ?? throw new ArgumentNullException(nameof(newsArticleRepository));
    }

    public async Task<string> CreateLikeAsync(NewsLikeCreateDto createDto)
    {
        if (createDto == null)
            throw new ArgumentNullException(nameof(createDto));
        if (string.IsNullOrEmpty(createDto.UserId))
            throw new ArgumentNullException(nameof(createDto.UserId));
        if (string.IsNullOrEmpty(createDto.ArticleId))
            throw new ArgumentNullException(nameof(createDto.ArticleId));
        
        return await _newsLikeRepository.CreateAsync(new NewsLike
        {
            UserId = createDto.UserId,
            NewsArticleId = createDto.ArticleId 
        }) ?? throw new InvalidOperationException("Failed to create like.");
    }

    public async Task<string> DeleteLikeByIdAsync(string likeId)
    {
        return await _newsLikeRepository.DeleteByIdAsync(likeId)
            ?? throw new InvalidOperationException($"Failed to delete like with id: {likeId}");
    }

    public async Task<string> DeleteLikeByUserIdAndArticleIdAsync(string userId, string articleId)
    {
        return await _newsLikeRepository.DeleteByUserIdAndArticleId(userId, articleId)
            ?? throw new InvalidOperationException($"Failed to delete like for user: {userId} and article: {articleId}");
    }

    public async Task<NewsLike> GetLikeAsNoTrackingAsync(string likeId)
    {
        return await _newsLikeRepository.GetAsNoTrackingAsync(likeId)
            ?? throw new InvalidOperationException($"Like not found with id: {likeId}");
    }

    public async Task<NewsLike> GetLikeByUserIdAndArticleId(string userId, string articleId)
    {
        return await _newsLikeRepository.GetByUserIdAndArticleId(userId, articleId)
            ?? throw new InvalidOperationException($"Like not found with articleId: {articleId} and userId: {userId}");
    }

    public async Task<IEnumerable<NewsArticle>> GetLikedNewsByUserIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        return await _newsLikeRepository.GetLikedNewsByUserIdAsync(userId);
    }

    public async Task<int> GetLikesCountByArticleIdAsync(string articleId)
    {
        if (string.IsNullOrEmpty(articleId))
            throw new ArgumentNullException(nameof(articleId));
        
        return await _newsLikeRepository.GetLikesCountByArticleIdAsync(articleId);
    }

    public async Task<bool> IsArticleLikedByUserAsync(string userId, string articleId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));
        if (string.IsNullOrEmpty(articleId))
            throw new ArgumentNullException(nameof(articleId));

        return await _newsLikeRepository.IsArticleLikedByUserAsync(userId, articleId);
    }
}
