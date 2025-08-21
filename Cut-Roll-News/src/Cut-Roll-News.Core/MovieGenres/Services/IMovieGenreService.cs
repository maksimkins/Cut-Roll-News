namespace Cut_Roll_News.Core.MovieGenres.Services;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Genres.Models;
using Cut_Roll_News.Core.MovieGenres.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;

public interface IMovieGenreService
{
    Task<Guid?> DeleteMovieGenreAsync(MovieGenreDto? dto);
    Task<Guid> CreateMovieGenreAsync(MovieGenreDto? dto);
    Task<bool> DeleteMovieGenreRangeByMovieId(Guid? movieId);
    Task<IEnumerable<Genre>> GetGenresByMovieIdAsync(Guid? movieId);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByGenreIdAsync(MovieSearchByGenreDto? dto);
    public Task<bool> BulkCreateMovieGenreAsync(IEnumerable<MovieGenreDto>? toCreate);
    public Task<bool> BulkDeleteMovieGenreAsync(IEnumerable<MovieGenreDto>? toDelete);
}



