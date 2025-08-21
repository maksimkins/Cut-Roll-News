namespace Cut_Roll_News.Core.MovieProductionCountries.Service;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Countries.Models;
using Cut_Roll_News.Core.MovieProductionCountries.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;

public interface IMovieProductionCountryService
{
    Task<Guid> CreateMovieProductionCountryAsync(MovieProductionCountryDto? dto);
    Task<Guid> DeleteMovieProductionCountryAsyncMovieProductionCountryAsync(MovieProductionCountryDto? dto);
    Task<bool> DeleteMovieProductionCountryRangeByMovieId(Guid? movieId);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByCountryIdAsync(MovieSearchByCountryDto? movieSearchByCountryDto);
    Task<IEnumerable<Country>> GetCountriesByMovieIdAsync(Guid? movieId);
    Task<bool> BulkCreateMovieProductionCountryAsync(IEnumerable<MovieProductionCountryDto>? toCreate);
    Task<bool> BulkDeleteMovieProductionCountryAsync(IEnumerable<MovieProductionCountryDto>? toDelete);
}


