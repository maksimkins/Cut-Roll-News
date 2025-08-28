using Cut_Roll_News.Core.Blob.Managers;
using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Genres.Services;
using Cut_Roll_News.Core.Keywords.Services;
using Cut_Roll_News.Core.Movies.Service;
using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsArticles.Repositories;
using Cut_Roll_News.Core.NewsArticles.Services;
using Cut_Roll_News.Core.NewsReferences.Dtos;
using Cut_Roll_News.Core.NewsReferences.Enums;
using Cut_Roll_News.Core.NewsReferences.Models;
using Cut_Roll_News.Core.NewsReferences.Repositories;
using Cut_Roll_News.Core.People.Service;
using Cut_Roll_News.Core.ProductionCompanies.Service;
using Cut_Roll_News.Core.Users.Dtos;
using Cut_Roll_News.Core.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_News.Infrastructure.NewsArticles.Services;

public class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _articleRepository;
    private readonly INewsReferenceRepository _referenceRepository;
    private readonly BaseBlobImageManager<Guid> _newsBlobManager;
    private readonly IMovieService _movieService;
    private readonly IPersonService _personService;
    private readonly IGenreService _genreService;
    private readonly IProductionCompanyService _productionCompanyService;
    private readonly IKeywordService _keywordService;
    private readonly IUserService _userService;


    public NewsArticleService(INewsArticleRepository articleRepository, INewsReferenceRepository referenceRepository,
        BaseBlobImageManager<Guid> newsBlobManager, IMovieService movieService, IPersonService personService, IGenreService genreService,
        IProductionCompanyService productionCompanyService, IKeywordService keywordService, IUserService userService)
    {
        _keywordService = keywordService ?? throw new ArgumentNullException(nameof(newsBlobManager));
        _productionCompanyService = productionCompanyService ?? throw new ArgumentNullException(nameof(productionCompanyService));
        _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        _newsBlobManager = newsBlobManager ?? throw new ArgumentNullException(nameof(newsBlobManager));
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        _referenceRepository = referenceRepository ?? throw new ArgumentNullException(nameof(referenceRepository));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<Guid> CreateArticleAsync(NewsArticleCreateDto? createDto)
    {
        if (createDto == null)
            throw new ArgumentNullException(nameof(createDto));

        if (string.IsNullOrEmpty(createDto.AuthorId))
            throw new ArgumentNullException(nameof(createDto.AuthorId));

        var foundUser = await _userService.GetUserByIdAsync(createDto.AuthorId)
            ?? throw new ArgumentException($"there is no user with id: {createDto.AuthorId}");

        var article = new NewsArticle
        {
            Title = !string.IsNullOrEmpty(createDto.Title) ? createDto.Title
                : throw new ArgumentNullException(nameof(createDto.Title)),
            Content = !string.IsNullOrEmpty(createDto.Content) ? createDto.Content
                : throw new ArgumentNullException(nameof(createDto.Content)),
            AuthorId = createDto.AuthorId,
            CreatedAt = DateTime.UtcNow,
        };

        var createdId = await _articleRepository.CreateAsync(article)
            ?? throw new InvalidOperationException("Failed to create article.");
        
        if (createDto.References is not null && createDto.References.Any())
        {
            foreach (var reference in createDto.References)
            {
                if (reference.ReferencedId == null || reference.ReferencedId == Guid.Empty)
                    throw new ArgumentNullException(nameof(reference.ReferencedId));
                object entity = reference.ReferenceType switch
                {
                    ReferenceType.movie => await _movieService.GetMovieByIdAsync(reference.ReferencedId.Value)
                        ?? throw new ArgumentException($"There is no movie with id: {reference.ReferencedId}"),

                    ReferenceType.people => await _personService.GetPersonByIdAsync(reference.ReferencedId.Value)
                        ?? throw new ArgumentException($"There is no person with id: {reference.ReferencedId}"),

                    ReferenceType.genre => await _genreService.GetGenreByIdAsync(reference.ReferencedId.Value)
                        ?? throw new ArgumentException($"There is no genre with id: {reference.ReferencedId}"),

                    ReferenceType.production_company => await _productionCompanyService.GetProductionCompanyByIdAsync(reference.ReferencedId.Value)
                        ?? throw new ArgumentException($"There is no production company with id: {reference.ReferencedId}"),

                    ReferenceType.keyword => await _keywordService.GetKeywordByIdAsync(reference.ReferencedId.Value)
                        ?? throw new ArgumentException($"There is no keyword with id: {reference.ReferencedId}"),

                    ReferenceType.news => await _articleRepository.GetAsNoTrackingAsync(reference.ReferencedId.Value)
                        ?? throw new ArgumentException($"There is no news with id: {reference.ReferencedId}"),

                    _ => throw new ArgumentException($"Unknown reference type: {reference.ReferenceType}")
                };

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

        var path = await _newsBlobManager.SetImageAsync(createdId, createDto.Photo);
        await _articleRepository.PatchPhotoAsync(createdId, path);

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

    public async Task<NewsArticleResponseDto?> GetArticleAsNoTrackingAsync(Guid? articleId)
    {
        if (articleId == null || articleId == Guid.Empty)
            throw new ArgumentNullException(nameof(articleId));

        return await _articleRepository.GetAsNoTrackingAsync(articleId.Value);
    }

    public async Task<PagedResult<NewsArticleResponseDto>> GetFilteredArticlesAsync(NewsArticleFilterDto? filterDto)
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

        var totalCount = await query.CountAsync();

        int page = Math.Max(1, filterDto.Page);
        int pageSize = Math.Max(1, filterDto.PageSize);

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return new PagedResult<NewsArticleResponseDto>
        {
            Data = await query.Include(n => n.Author).AsNoTracking().Select(na => new NewsArticleResponseDto
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
        }).ToListAsync(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

    }

    public async Task<PagedResult<NewsArticleResponseDto>> GetMostRecentArticlesAsync(NewsArticleRecentDto? recentDto)
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

        var totalCount = await query.CountAsync();

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        

        return new PagedResult<NewsArticleResponseDto>
        {
            Data = await query.Include(n => n.Author).AsNoTracking().Select(na => new NewsArticleResponseDto
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
        }).ToListAsync(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
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

        return await _articleRepository.UpdateAsync(new NewsArticle
        {
            Id = foundArticle.Id,
            AuthorId = foundArticle.AuthorId,
            Content = foundArticle.Content,
            Title = foundArticle.Title,
            PhotoPath = foundArticle.PhotoPath,
            UpdatedAt = DateTime.UtcNow,

        })
            ?? throw new InvalidOperationException($"Failed to update content for article with id: {newsId}");
    }

    public async Task<PagedResult<NewsArticleResponseDto>> GetArticlesAsync(NewsArticlePaginationDto? paginationDto)
    {
        if (paginationDto == null)
            throw new ArgumentNullException(nameof(paginationDto));

        var query = await _articleRepository.GetAllAsQueryableAsync();

        int page = paginationDto.Page <= 0 ? 1 : paginationDto.Page;
        int pageSize = paginationDto.PageSize <= 0 ? 10 : paginationDto.PageSize;

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

    public async Task<Guid> PatchPhotoAsync(Guid? articleId, IFormFile? photo)
    {
        if (articleId == null || articleId == Guid.Empty)
            throw new ArgumentNullException($"there is no article with id:{articleId}");
        if (photo == null)
            throw new ArgumentException($"there is no photo path to update");

        var photoPath = await _newsBlobManager.SetImageAsync(articleId.Value, photo);
        
        return await _articleRepository.PatchPhotoAsync(articleId.Value, photoPath);
    }
}
