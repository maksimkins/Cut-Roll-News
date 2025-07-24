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

    public async Task<Guid?> CreateAsync(NewsArticle newsArticle)
    {
        await _dbContext.NewsArticles.AddAsync(newsArticle);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? newsArticle.Id : null;
    }

    public async Task<int> DecrementLikes(Guid articleId)
    {
        var article = await _dbContext.NewsArticles.Where(a => a.Id == articleId).FirstOrDefaultAsync()
            ?? throw new ArgumentNullException(nameof(articleId), $"Article with id: {articleId} not found");
        article.LikesCount--;

        await _dbContext.SaveChangesAsync();
        return article.LikesCount; 
    }

    public async Task<Guid?> DeleteByIdAsync(Guid id)
    {
        var newsArticle = _dbContext.NewsArticles.Find(id);
        if (newsArticle == null)
        {
            throw new ArgumentNullException(nameof(newsArticle), $"News article not found, with id: {id}");
        }

        _dbContext.NewsArticles.Remove(newsArticle);

        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? newsArticle.Id : null;
    }

    public async Task<IQueryable<NewsArticle>> GetAllAsQueryableAsync()
    {
        return await Task.FromResult(_dbContext.NewsArticles
            .Include(nl => nl.NewsReferences)
            .Include(nl => nl.NewsLikes)
            .AsQueryable());
    }

    public async Task<NewsArticle?> GetAsNoTrackingAsync(Guid id)
    {
        return await _dbContext.NewsArticles.AsNoTracking().FirstOrDefaultAsync(na => na.Id == id);
    }

    public async Task<int> IncrementLikes(Guid articleId)
    {
        var article = await _dbContext.NewsArticles.Where(a => a.Id == articleId).FirstOrDefaultAsync()
            ?? throw new ArgumentNullException(nameof(articleId), $"Article with id: {articleId} not found");
        article.LikesCount++;

        await _dbContext.SaveChangesAsync();
        return article.LikesCount; 
    }

    public async Task<Guid?> UpdateAsync(NewsArticle entity)
    {
        _dbContext.NewsArticles.Update(entity);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? entity.Id : null;
    }
}
