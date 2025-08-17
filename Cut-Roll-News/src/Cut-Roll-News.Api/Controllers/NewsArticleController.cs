using Cut_Roll_News.Api.Common.Extensions.Controllers;
using Cut_Roll_News.Core.NewsArticles.Dtos;
using Cut_Roll_News.Core.NewsArticles.Services;
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

    [HttpPost]
    [Authorize(Roles = "Publisher, Admin")]
    public async Task<IActionResult> CreateNewsArticle([FromBody] NewsArticleCreateDto newsArticleCreateDto)
    {
        try
        {
            var newsId = await _newsArticleService.CreateArticleAsync(newsArticleCreateDto);
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
