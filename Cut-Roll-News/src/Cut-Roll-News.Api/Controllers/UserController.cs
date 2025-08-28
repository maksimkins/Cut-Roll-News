using Cut_Roll_News.Api.Common.Extensions.Controllers;
using Cut_Roll_News.Core.Users.Dtos;
using Cut_Roll_News.Core.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cut_Roll_News.Api.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] UserSearchDto dto)
        {
            try
            {
                var result = await _userService.SearchUsersAsync(dto);
                return Ok(result);
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
            return this.InternalServerError(ex.Message);
        }
        }
    }
