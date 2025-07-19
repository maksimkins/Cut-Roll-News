

using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsArticles.Repositories;
using Cut_Roll_News.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_News.Infrastructure.NewsArticles.Repositories;

public class NewsArticleEfCoreRepository : INewsArticleRepository
{
    private readonly NewsDbContext _dbContext;
    public NewsArticleEfCoreRepository(NewsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<string?> CreateAsync(NewsArticle newsArticle)
    {
        await _dbContext.NewsArticles.AddAsync(newsArticle);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? newsArticle.Id : null);
    }

    public async Task<int> DecrementLikes(string articleId)
    {
        var article = await _dbContext.NewsArticles.Where(a => a.Id == articleId).FirstOrDefaultAsync()
            ?? throw new ArgumentNullException(nameof(articleId), $"Article with id: {articleId} not found");
        article.LikesCount--;

        await _dbContext.SaveChangesAsync();
        return article.LikesCount; 
    }

    public async Task<string?> DeleteByIdAsync(string id)
    {
        var newsArticle = _dbContext.NewsArticles.Find(id);
        if (newsArticle == null)
        {
            throw new ArgumentNullException(nameof(newsArticle), $"News article not found, with id: {id}");
        }

        _dbContext.NewsArticles.Remove(newsArticle);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? id : null);
    }

    public async Task<IQueryable<NewsArticle>> GetAllAsQueryableAsync()
    {
        return await Task.FromResult(_dbContext.NewsArticles
            .Include(nl => nl.NewsReferences)
            .Include(nl => nl.NewsLikes)
            .AsQueryable());
    }

    public async Task<NewsArticle?> GetAsNoTrackingAsync(string id)
    {
        return await _dbContext.NewsArticles.AsNoTracking().FirstOrDefaultAsync(na => na.Id == id);
    }

    public async Task<int> IncrementLikes(string articleId)
    {
        var article = await _dbContext.NewsArticles.Where(a => a.Id == articleId).FirstOrDefaultAsync()
            ?? throw new ArgumentNullException(nameof(articleId), $"Article with id: {articleId} not found");
        article.LikesCount++;

        await _dbContext.SaveChangesAsync();
        return article.LikesCount; 
    }

    public async Task<string?> UpdateAsync(NewsArticle entity)
    {
        _dbContext.NewsArticles.Update(entity);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? entity.Id : null);
    }
}
