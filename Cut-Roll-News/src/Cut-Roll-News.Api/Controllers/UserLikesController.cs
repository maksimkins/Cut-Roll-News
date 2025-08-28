namespace Cut_Roll_News.Api.Controllers;

using System.Security.Claims;
using Cut_Roll_News.Api.Common.Extensions.Controllers;
using Cut_Roll_News.Core.NewsLikes.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users/{userId}/liked-news")]
public class UserLikesController : ControllerBase
{
    private readonly INewsLikeService _newsLikeService;

    public UserLikesController(INewsLikeService newsLikeService)
    {
        _newsLikeService = newsLikeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLikedNews(string userId, int page, int pageSize)
    {
        try
        {
            var newsArticles = await _newsLikeService.GetLikedNewsByUserIdAsync(userId, page, pageSize);
            return Ok(newsArticles);
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