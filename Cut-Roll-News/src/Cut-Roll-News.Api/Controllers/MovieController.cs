namespace Cut_Roll_News.Api.Controllers;

using Cut_Roll_News.Api.Common.Extensions.Controllers;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.Movies.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] MovieSearchRequest? request)
    {
        try
        {
            var result = await _movieService.SearchMovieAsync(request);
            return Ok(result);
        }
        catch (ArgumentNullException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        catch (Exception ex) { return this.InternalServerError(ex.Message); }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _movieService.GetMovieByIdAsync(id);
            return result is not null ? Ok(result) : NotFound($"Movie with id {id} not found.");
        }
        catch (ArgumentNullException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        catch (Exception ex) { return this.InternalServerError(ex.Message); }
    }

    [Authorize(Roles = "Admin,Publisher")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MovieCreateDto? dto)
    {
        try
        {
            var id = await _movieService.CreateMovieAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (ArgumentNullException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        catch (Exception ex) { return this.InternalServerError(ex.Message); }
    }

    [Authorize(Roles = "Admin,Publisher")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MovieUpdateDto? dto)
    {
        try
        {
            var id = await _movieService.UpdateMovieAsync(dto);
            return Ok(id);
        }
        catch (ArgumentNullException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        catch (Exception ex) { return this.InternalServerError(ex.Message); }
    }

    [Authorize(Roles = "Admin,Publisher")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deletedId = await _movieService.DeleteMovieByIdAsync(id);
            return Ok(deletedId);
        }
        catch (ArgumentNullException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
        catch (Exception ex) { return this.InternalServerError(ex.Message); }
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        try
        {
            var count = await _movieService.CountMoviesAsync();
            return Ok(count);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

}
