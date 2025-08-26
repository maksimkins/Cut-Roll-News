using System.Security.Claims;
using Cut_Roll_News.Api.Common.Extensions.Controllers;
using Cut_Roll_News.Core.NewsLikes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cut_Roll_News.Api.Controllers;

[ApiController]
[Route("news/{newsId}/likes")]
public class NewsLikeController : ControllerBase
{
    private readonly INewsLikeService _newsLikeService;
    public NewsLikeController(INewsLikeService newsLikeService)
    {
        _newsLikeService = newsLikeService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> IsLiked(Guid newsId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var liked = await _newsLikeService.IsArticleLikedByUserAsync(userId, newsId);

            return Ok(liked);
        }
        catch (ArgumentException ex)
        {
            return this.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ToggleLikeNews(Guid newsId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var likeId = await _newsLikeService.IsArticleLikedByUserAsync(userId, newsId)
                ? await _newsLikeService.DeleteLikeByUserIdAndArticleIdAsync(userId, newsId)
                : await _newsLikeService.CreateLikeAsync(userId, newsId);

            return Ok(likeId);
        }
        catch (ArgumentException ex)
        {
            return this.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetLikedNews()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var likedNews = await _newsLikeService.GetLikedNewsByUserIdAsync(userId);
            return Ok(likedNews);
        }
        catch (ArgumentException ex)
        {
            return this.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }
}
