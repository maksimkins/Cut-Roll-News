
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
    public async Task<Guid?> CreateAsync(NewsReference newsReference)
    {
        await _dbContext.NewsReferences.AddAsync(newsReference);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? newsReference.NewsArticleId : null;
    }

    public async Task<Guid?> DeleteByArticleIdAndReferenceIdAsync(Guid articleId, Guid referencedId)
    {
        await _dbContext.NewsReferences
            .Where(nr => nr.NewsArticleId == articleId && nr.ReferencedId == referencedId)
            .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();

        return articleId;
    }

    public async Task<IEnumerable<NewsReference>> GetAllByArticleIdAsync(Guid articleId)
    {
        var query = _dbContext.NewsReferences.AsQueryable();
        
        return await query.Where(nr => nr.NewsArticleId == articleId).ToListAsync();
    }

    public async Task<int> GetCountByArticleIdAsync(Guid articleId)
    {
        return await _dbContext.NewsReferences.CountAsync(nr => nr.NewsArticleId == articleId);
    }

    public async Task<bool> IsReferenceExistsAsync(Guid referencedId, Guid articleId)
    {
        return await _dbContext.NewsReferences.AnyAsync(nr => nr.ReferencedId == referencedId && nr.NewsArticleId == articleId);
    }
}
