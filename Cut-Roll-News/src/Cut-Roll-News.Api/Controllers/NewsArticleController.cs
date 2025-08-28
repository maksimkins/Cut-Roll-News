using System.Security.Claims;
using System.Text.Json;
using Cut_Roll_News.Api.Common.Dtos;
using Cut_Roll_News.Api.Common.Extensions.Controllers;
using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Services;
using Cut_Roll_News.Core.NewsReferences.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cut_Roll_News.Api.Controllers;

[ApiController]
[Route("news")]
public class NewsArticleController : ControllerBase
{
    private readonly INewsArticleService _newsArticleService;
    public NewsArticleController(INewsArticleService newsArticleService)
    {
        _newsArticleService = newsArticleService;
    }

    [Consumes("multipart/form-data")]
    [HttpPost]
    [Authorize(Roles = "Publisher, Admin")]
    public async Task<IActionResult> CreateNewsArticle([FromForm] NewsArticleCreationEndpointDto newsArticleCreateDto)
    {
        try
        {
            var references = JsonSerializer.Deserialize<List<NewsReferenceCreateDto>>(newsArticleCreateDto.ReferencesJson);

            var newsId = await _newsArticleService.CreateArticleAsync(new NewsArticleCreateDto
            {
                Title = newsArticleCreateDto.Title,
                Photo = newsArticleCreateDto.Photo,
                References = references ?? throw new ArgumentNullException(nameof(references)),
                AuthorId = newsArticleCreateDto.AuthorId,
                Content = newsArticleCreateDto.Content
            });
            return Ok(newsId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpGet("{newsId}")]
    public async Task<IActionResult> GetNewsArticle(Guid newsId)
    {
        try
        {
            var newsArticle = await _newsArticleService.GetArticleAsNoTrackingAsync(newsId);
            if (newsArticle == null)
            {
                return NotFound();
            }
            return Ok(newsArticle);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPut("{newsId}")]
    [Authorize(Roles = "Publisher, Admin")]
    public async Task<IActionResult> UpdateNewsArticle(Guid newsId, [FromBody] NewsArticleUpdateContentDto newsArticleUpdateDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var news = await _newsArticleService.GetArticleAsNoTrackingAsync(newsId);
            if (userRole == "Publisher" && news != null)
                news = news.AuthorId == userId ? news : throw new ArgumentException("publisher doesnt posess news");
                
            var updatedNewsId = await _newsArticleService.UpdateAtricleContentAsync(newsId, newsArticleUpdateDto);
            return Ok(updatedNewsId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpDelete("{newsId}")]
    [Authorize(Roles = "Publisher, Admin")]
    public async Task<IActionResult> DeleteNewsArticle(Guid newsId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var news = await _newsArticleService.GetArticleAsNoTrackingAsync(newsId);
            if (userRole == "Publisher" && news != null)
                news = news.AuthorId == userId ? news : throw new ArgumentException("publisher doesnt posess news");

            await _newsArticleService.DeleteArticleByIdAsync(newsId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPatch("{newsId}/photo")]
    [Authorize(Roles = "Publisher, Admin")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> PatchPhoto(Guid newsId, [FromForm] IFormFile? photo)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var news = await _newsArticleService.GetArticleAsNoTrackingAsync(newsId);
            if (userRole == "Publisher" && news != null)
                news = news.AuthorId == userId ? news : throw new ArgumentException("publisher doesnt posess news");

            var updatedArticleId = await _newsArticleService.PatchPhotoAsync(newsId, photo);
            return Ok(updatedArticleId);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("pagination")]
    public async Task<IActionResult> GetNewsArticles([FromBody] NewsArticlePaginationDto paginationDto)
    {
        try
        {
            var articles = await _newsArticleService.GetArticlesAsync(paginationDto);
            return Ok(articles);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost("filter")]
    public async Task<IActionResult> GetFilteredArticles([FromBody] NewsArticleFilterDto filterDto)
    {
        try
        {
            var articles = await _newsArticleService.GetFilteredArticlesAsync(filterDto);
            return Ok(articles);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost("recent")]
    public async Task<IActionResult> GetRecentArticles([FromBody] NewsArticleRecentDto recentDto)
    {
        try
        {
            var articles = await _newsArticleService.GetMostRecentArticlesAsync(recentDto);
            return Ok(articles);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

}
