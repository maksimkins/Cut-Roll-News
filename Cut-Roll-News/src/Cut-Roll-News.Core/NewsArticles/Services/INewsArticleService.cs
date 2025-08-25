using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsReferences.Dtos;
using Microsoft.AspNetCore.Http;

namespace Cut_Roll_News.Core.NewsArticles.Services;

public interface INewsArticleService
{
    public Task<Guid> CreateArticleAsync(NewsArticleCreateDto? createDto);
    public Task<Guid> DeleteArticleByIdAsync(Guid? articleId);
    public Task<NewsArticle?> GetArticleAsNoTrackingAsync(Guid? articleId);
    public Task<Guid> UpdateAtricleContentAsync(Guid? articleId, NewsArticleUpdateContentDto? updateDto);
    public Task<Guid> DeleteArticleReferences(Guid? articleId, IEnumerable<NewsReferenceDeleteDto>? referencesToDelete);
    public Task<Guid> CreateArticleReferences(Guid? articleId, IEnumerable<NewsReferenceCreateDto>? referencesToCreate);
    public Task<PagedResult<NewsArticle>> GetMostRecentArticlesAsync(NewsArticleRecentDto? recentDto);
    public Task<PagedResult<NewsArticle>> GetFilteredArticlesAsync(NewsArticleFilterDto? filterDto);
    public Task<PagedResult<NewsArticle>> GetArticlesAsync(NewsArticlePaginationDto? paginationDto);
    public Task<Guid> PatchPhotoAsync(Guid? articleId, IFormFile? logo);
}
