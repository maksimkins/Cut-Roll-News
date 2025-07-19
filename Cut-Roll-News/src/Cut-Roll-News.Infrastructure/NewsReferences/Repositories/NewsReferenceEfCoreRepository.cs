
using Cut_Roll_News.Core.NewsReferences.Models;
using Cut_Roll_News.Core.NewsReferences.Repositories;
using Cut_Roll_News.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_News.Infrastructure.NewsReferences.Repositories;

public class NewsReferenceEfCoreRepository : INewsReferenceRepository
{
    private readonly NewsDbContext _dbContext;

    public NewsReferenceEfCoreRepository(NewsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<string?> CreateAsync(NewsReference newsReference)
    {
        await _dbContext.NewsReferences.AddAsync(newsReference);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? newsReference.Id : null);
    }

    public async Task<string?> DeleteByArticleIdAndReferenceIdAsync(string articleId, string referencedId)
    {
        var newsReference = await _dbContext.NewsReferences
            .Where(nr => nr.NewsArticleId == articleId && nr.ReferenceId == referencedId)
            .FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(NewsReference),
            $"News reference not found for the given articleId {articleId} and referencedId {referencedId}.");

        _dbContext.NewsReferences.Remove(newsReference);

        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? newsReference.Id : null);
    }

    public async Task<string?> DeleteByIdAsync(string id)
    {
        var newsReference = _dbContext.NewsReferences.Find(id);
        if (newsReference == null)
        {
            throw new ArgumentNullException(nameof(newsReference), $"News reference not found, with id: {id}");
        }

        _dbContext.NewsReferences.Remove(newsReference);
        return await _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? id : null);
    }

    public async Task<IEnumerable<NewsReference>> GetAllByArticleIdAsync(string articleId)
    {
        var query = _dbContext.NewsReferences.AsQueryable();
        
        return await query.Where(nr => nr.NewsArticleId == articleId).ToListAsync();
    }

    public async Task<NewsReference?> GetAsNoTrackingAsync(string id)
    {
        return await _dbContext.NewsReferences.AsNoTracking().FirstOrDefaultAsync(nr => nr.Id == id);
    }

    public async Task<NewsReference?> GetByIdAsync(string id)
    {
        return await _dbContext.NewsReferences.FirstOrDefaultAsync(nr => nr.Id == id);
    }

    public async Task<int> GetCountByArticleIdAsync(string articleId)
    {
        return await _dbContext.NewsReferences.CountAsync(nr => nr.NewsArticleId == articleId);
    }

    public async Task<bool> IsReferenceExistsAsync(string referenceId, string articleId)
    {
        return await _dbContext.NewsReferences.AnyAsync(nr => nr.Id == referenceId && nr.NewsArticleId == articleId);
    }
}
