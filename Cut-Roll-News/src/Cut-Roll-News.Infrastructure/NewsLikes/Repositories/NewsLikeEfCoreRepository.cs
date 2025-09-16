namespace Cut_Roll_News.Infrastructure.NewsLikes.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsLikes.Repositories;
using Cut_Roll_News.Core.Users.Dtos;
using Cut_Roll_News.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

public class NewsLikeEfCoreRepository : INewsLikeRepository
{
    private readonly NewsDbContext _dbContext;
    public NewsLikeEfCoreRepository(NewsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<Guid?> CreateAsync(NewsLike newsLike)
    {
        await _dbContext.NewsLikes.AddAsync(newsLike);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? newsLike.NewsArticleId : null;
    }


    public async Task<Guid?> DeleteByUserIdAndArticleId(string userId, Guid articleId)
    {
        var newsLike = await _dbContext.NewsLikes.Where(nl => nl.UserId == userId && nl.NewsArticleId == articleId).FirstOrDefaultAsync();
        if (newsLike == null)
        {
            throw new ArgumentNullException(nameof(newsLike), $"Like not found for user: {userId} and article: {articleId}");
        }
        _dbContext.NewsLikes.Remove(newsLike);

        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? articleId : null;
    }

    public Task<bool> ExistsAsync(string userId, Guid articleId)
    {
        return _dbContext.NewsLikes
            .AnyAsync(nl => nl.UserId == userId && nl.NewsArticleId == articleId);
    }

    public async Task<NewsLike?> GetByUserIdAndArticleId(string userId, Guid articleId)
    {
        return await _dbContext.NewsLikes.AsNoTracking().Where(nl => nl.UserId == userId && nl.NewsArticleId == articleId).FirstOrDefaultAsync();
    }

    public async Task<PagedResult<NewsArticleResponseDto>> GetLikedNewsByUserIdAsync(string userId, int page, int pageSize)
    {
        var query = _dbContext.NewsLikes
            .Where(nl => nl.UserId == userId)
            .Include(nl => nl.NewsArticle).ThenInclude(na => na!.Author)
            .Select(nl => nl.NewsArticle!); 
        
        var totalCount = await query.CountAsync();
        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        var result = await query.Include(n => n.Author).AsNoTracking().Select(na => new NewsArticleResponseDto
        {
            Id = na.Id,
            AuthorId = na.Author!.Id,
            Author = new UserSimplified
            {
                Id = na.AuthorId,
                UserName = na.Author.UserName,
                Email = na.Author.Email,
                AvatarPath = na.Author.AvatarPath
            },
            CreatedAt = na.CreatedAt,
            UpdatedAt = na.UpdatedAt,
            Title = na.Title,
            Content = na.Content,
            PhotoPath = na.PhotoPath,
            LikesCount = na.LikesCount,
            NewsReferences = na.NewsReferences
        }).ToListAsync();

        return new PagedResult<NewsArticleResponseDto>
        {
            Data = result,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public Task<int> GetLikesCountByArticleIdAsync(Guid articleId)
    {
        return _dbContext.NewsLikes
            .CountAsync(nl => nl.NewsArticleId == articleId);
    }
}
