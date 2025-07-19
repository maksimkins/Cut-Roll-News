
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_News.Infrastructure.NewsLikes.Repositories;

public class NewsLikeEfCoreRepository : INewsLikeRepository
{
    private readonly NewsDbContext _dbContext;
    public NewsLikeEfCoreRepository(NewsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<string?> CreateAsync(NewsLike newsLike)
    {
        if (_dbContext.NewsArticles.Any(n => n.Id == newsLike.NewsArticleId))
            throw new ArgumentException(nameof(newsLike.NewsArticleId), "News article not found for the like.");

        await _dbContext.NewsLikes.AddAsync(newsLike);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? newsLike.Id : null);
    }

    public async Task<string?> DeleteByIdAsync(string id)
    {
        var newsLike = _dbContext.NewsLikes.Find(id);
        if (newsLike == null)
        {
            throw new ArgumentNullException(nameof(newsLike), $"Like not found, with id: {id}");
        }

        _dbContext.NewsLikes.Remove(newsLike);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? id : null);
    }

    public async Task<string?> DeleteByUserIdAndArticleId(string userId, string articleId)
    {
        var newsLike = await _dbContext.NewsLikes.Where(nl => nl.UserId == userId && nl.NewsArticleId == articleId).FirstOrDefaultAsync();
        if (newsLike == null)
        {
            throw new ArgumentNullException(nameof(newsLike), $"Like not found for user: {userId} and article: {articleId}");
        }
        _dbContext.NewsLikes.Remove(newsLike);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? newsLike.Id : null);
    }

    public async Task<NewsLike?> GetAsNoTrackingAsync(string id)
    {
        return await _dbContext.NewsLikes.Include(nl => nl.NewsArticle).AsNoTracking().FirstOrDefaultAsync(nl => nl.Id == id);
    }

    public async Task<NewsLike?> GetByUserIdAndArticleId(string userId, string articleId)
    {
        return await _dbContext.NewsLikes.AsNoTracking().Where(nl => nl.UserId == userId && nl.NewsArticleId == articleId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<NewsArticle>> GetLikedNewsByUserIdAsync(string userId)
    {
        var newsArticles = await _dbContext.NewsLikes
            .Where(nl => nl.UserId == userId)
            .Include(nl => nl.NewsArticle)
            .Select(nl => nl.NewsArticle!) 
            .AsNoTracking()
            .ToListAsync();
        
        return newsArticles;
    }

    public Task<int> GetLikesCountByArticleIdAsync(string articleId)
    {
        return _dbContext.NewsLikes
            .CountAsync(nl => nl.NewsArticleId == articleId);
    }

    public Task<bool> IsArticleLikedByUserAsync(string userId, string articleId)
    {
        return _dbContext.NewsLikes
            .AnyAsync(nl => nl.UserId == userId && nl.NewsArticleId == articleId);
    }
}
