using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsArticles.Repositories;
using Cut_Roll_News.Core.NewsArticles.Services;
using Cut_Roll_News.Core.NewsReferences.Dtos;
using Cut_Roll_News.Core.NewsReferences.Models;
using Cut_Roll_News.Core.NewsReferences.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_News.Infrastructure.NewsArticles.Services;

public class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _articleRepository;
    private readonly INewsReferenceRepository _referenceRepository;
    public NewsArticleService(INewsArticleRepository articleRepository, INewsReferenceRepository referenceRepository)
    {
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        _referenceRepository = referenceRepository ?? throw new ArgumentNullException(nameof(referenceRepository));
    }

    public async Task<Guid> CreateArticleAsync(NewsArticleCreateDto? createDto)
    {
        if (createDto == null)
            throw new ArgumentNullException(nameof(createDto));

        var article = new NewsArticle
        {
            Title = !string.IsNullOrEmpty(createDto.Title) ? createDto.Title
                : throw new ArgumentNullException(nameof(createDto.Title)),
            Content = !string.IsNullOrEmpty(createDto.Content) ? createDto.Content
                : throw new ArgumentNullException(nameof(createDto.Content)),
            AuthorId = !string.IsNullOrEmpty(createDto.AuthorId) ? createDto.AuthorId
                : throw new ArgumentNullException(nameof(createDto.AuthorId)),
            CreatedAt = DateTime.UtcNow,
        };

        var createdId = await _articleRepository.CreateAsync(article) ?? throw new InvalidOperationException("Failed to create article.");
        if (createDto.References is not null && createDto.References.Any())
        {
            foreach (var reference in createDto.References)
            {
                var newsReference = new NewsReference
                {
                    ReferencedUrl = reference.ReferenceUrl,
                    ReferenceType = reference.ReferenceType,
                    NewsArticleId = createdId,
                    ReferencedId = reference.ReferencedId ?? throw new ArgumentNullException(nameof(reference.ReferencedId)),
                };

                await _referenceRepository.CreateAsync(newsReference);
            }
        }

        return createdId;
    }

    public async Task<Guid> CreateArticleReferences(Guid? articleId, IEnumerable<NewsReferenceCreateDto>? referencesToCreate)
    {
        if (referencesToCreate is not null && referencesToCreate.Any())
        {
            foreach (var reference in referencesToCreate)
            {
                var newsReference = new NewsReference
                {
                    ReferenceType = reference.ReferenceType,
                    NewsArticleId = articleId ?? throw new ArgumentNullException(nameof(reference.ReferencedId)),
                    ReferencedId = reference.ReferencedId ?? throw new ArgumentNullException(nameof(reference.ReferencedId)),
                };

                _ = await _referenceRepository.CreateAsync(newsReference)
                    ?? throw new InvalidOperationException($"Failed to create reference with id: {reference.ReferencedId}");
            }
        }

        return articleId ?? throw new ArgumentNullException(nameof(articleId));
    }

    public async Task<Guid> DeleteArticleByIdAsync(Guid? articleId)
    {
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        return await _articleRepository.DeleteByIdAsync(articleId.Value) ?? throw new InvalidOperationException($"Failed to delete article with id: {articleId}");
    }

    public async Task<Guid> DeleteArticleReferences(Guid? articleId, IEnumerable<NewsReferenceDeleteDto>? referencesToDelete)
    {
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        if (referencesToDelete is not null && referencesToDelete.Any())
        {
            foreach (var reference in referencesToDelete)
            {
                if (reference.ReferencedId == null)
                    throw new ArgumentNullException(nameof(reference.ReferencedId));

                var exists = await _referenceRepository.ExistsAsync(reference.ReferencedId.Value, articleId.Value);
                if (exists)
                {
                    _ = await _referenceRepository.DeleteByArticleIdAndReferenceIdAsync(articleId.Value, reference.ReferencedId.Value)
                        ?? throw new InvalidOperationException($"Failed to delete reference with id: {reference.ReferencedId} for article with id: {articleId}");
                }
            }
        }

        return articleId ?? throw new ArgumentNullException(nameof(articleId));
    }

    public async Task<NewsArticle?> GetArticleAsNoTrackingAsync(Guid? articleId)
    {
        if (articleId == null)
            throw new ArgumentNullException(nameof(articleId));

        return await _articleRepository.GetAsNoTrackingAsync(articleId.Value);
    }

    public async Task<IEnumerable<NewsArticle>> GetFilteredArticlesAsync(NewsArticleFilterDto? filterDto)
    {
        if (filterDto == null)
            throw new ArgumentNullException(nameof(filterDto));

        var query = await _articleRepository.GetAllAsQueryableAsync();
        if (!string.IsNullOrWhiteSpace(filterDto.Query))
        {
            string queryText = filterDto.Query.Trim().ToLower();
            query = query.Where(a =>
                a.Title.ToLower().Contains(queryText) ||
                a.Content.ToLower().Contains(queryText));
        }
        if (filterDto.From.HasValue)
            query = query.Where(a => a.CreatedAt >= filterDto.From.Value);

        if (filterDto.To.HasValue)
            query = query.Where(a => a.CreatedAt <= filterDto.To.Value);

        if (filterDto.ReferenceToSearch is { Count: > 0 })
        {
            foreach (var refSearch in filterDto.ReferenceToSearch)
            {
                var localRef = refSearch;

                query = query.Where(a =>
                    a.NewsReferences != null &&
                    a.NewsReferences.Any(r =>
                        r.ReferenceType == localRef.ReferenceType &&
                        r.ReferencedId == localRef.ReferencedId));
            }
        }

        query = query.OrderByDescending(a => a.CreatedAt);

        int page = Math.Max(1, filterDto.Page);
        int pageSize = Math.Max(1, filterDto.PageSize);

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<NewsArticle>> GetMostRecentArticlesAsync(NewsArticleRecentDto? recentDto)
    {
        if (recentDto == null)
            throw new ArgumentNullException(nameof(recentDto));

        var query = await _articleRepository.GetAllAsQueryableAsync();

        if (recentDto.Days.HasValue && recentDto.Days.Value > 0)
        {
            var fromDate = DateTime.UtcNow.AddDays(-recentDto.Days.Value);
            query = query.Where(a => a.CreatedAt >= fromDate);
        }
        query = query.OrderByDescending(a => a.CreatedAt);

        int page = recentDto.Page <= 0 ? 1 : recentDto.Page;
        int pageSize = recentDto.PageSize <= 0 ? 10 : recentDto.PageSize;

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var result = await query.AsNoTracking().ToListAsync();

        return result;
    }

    public async Task<Guid> UpdateAtricleContentAsync(Guid? newsId, NewsArticleUpdateContentDto? updateDto)
    {
        if (updateDto == null)
            throw new ArgumentNullException(nameof(updateDto));

        if (newsId == null)
            throw new ArgumentNullException(nameof(newsId));

        if (string.IsNullOrEmpty(updateDto.NewContent) && string.IsNullOrEmpty(updateDto.NewTitle))
            throw new ArgumentException("At least one of NewContent or NewTitle must be provided.");

        var foundArticle = await _articleRepository.GetAsNoTrackingAsync(newsId.Value) ??
            throw new InvalidOperationException($"Article not found with id: {newsId}");

        foundArticle.Content = updateDto.NewContent ?? foundArticle.Content;
        foundArticle.Title = updateDto.NewTitle ?? foundArticle.Title;

        return await _articleRepository.UpdateAsync(foundArticle)
            ?? throw new InvalidOperationException($"Failed to update content for article with id: {newsId}");
    }

    public async Task<IEnumerable<NewsArticle>> GetArticlesAsync(NewsArticlePaginationDto? paginationDto)
    {
        if (paginationDto == null)
            throw new ArgumentNullException(nameof(paginationDto));

        var query = await _articleRepository.GetAllAsQueryableAsync();

        int page = paginationDto.Page <= 0 ? 1 : paginationDto.Page;
        int pageSize = paginationDto.PageSize <= 0 ? 10 : paginationDto.PageSize;

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var result = await query.AsNoTracking().ToListAsync();

        return result;
    }
}
