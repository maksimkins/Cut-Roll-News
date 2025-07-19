using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsReferences.Dtos;

namespace Cut_Roll_News.Core.NewsArticles.Services;

public interface INewsArticleService
{
    public Task<string> CreateArticleAsync(NewsArticleCreateDto createDto);
    public Task<string> DeleteArticleByIdAsync(string? articleId);
    public Task<NewsArticle> GetArticleAsNoTrackingAsync(string? articleId);
    public Task<string> UpdateAtricleContentAsync(NewsArticleUpdateContentDto updateDto);
    public Task<string> DeleteArticleReferences(string? articleId, IEnumerable<NewsReferenceDeleteDto> referencesToDelete);
    public Task<string> CreateArticleReferences(string? articleId, IEnumerable<NewsReferenceCreateDto> referencesToCreate);
    public Task<IEnumerable<NewsArticle>> GetMostRecentArticlesAsync(NewsArticleRecentDto recentDto);
    public Task<IEnumerable<NewsArticle>> GetFilteredArticlesAsync(NewsArticleFilterDto filterDto);
}
