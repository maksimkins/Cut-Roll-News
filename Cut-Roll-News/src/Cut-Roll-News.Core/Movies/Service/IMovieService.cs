namespace Cut_Roll_News.Core.Movies.Service;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.Movies.Models;

public interface IMovieService
{
    Task<PagedResult<MovieSimplifiedDto>> SearchMovieAsync(MovieSearchRequest? movieSearchRequest);
    Task<Movie?> GetMovieByIdAsync(Guid? id);
    Task<Guid> UpdateMovieAsync(MovieUpdateDto? dto);
    Task<Guid> DeleteMovieByIdAsync(Guid? id);
    Task<Guid> CreateMovieAsync(MovieCreateDto? dto);
    Task<int> CountMoviesAsync();
}

