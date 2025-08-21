using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Countries.Models;
using Cut_Roll_News.Core.MovieOriginCountries.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;

namespace Cut_Roll_News.Core.MovieOriginCountries.Service;

public interface IMovieOriginCountryService
{
    Task<Guid> CreateMovieOriginCountryAsync(MovieOriginCountryDto? dto);
    Task<Guid> DeleteMovieOriginCountryAsync(MovieOriginCountryDto? dto);
    Task<bool> DeleteMovieOriginCountryRangeByMovieIdAsync(Guid? movieId);
    Task<IEnumerable<Country>> GetCountriesByMovieIdAsync(Guid? movieId);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByOriginCountryIdAsync(MovieSearchByCountryDto? movieSearchByCountryDto);
    Task<bool> BulkCreateMovieOriginCountryAsync(IEnumerable<MovieOriginCountryDto>? toCreate);
    Task<bool> BulkDeleteMovieOriginCountryAsync(IEnumerable<MovieOriginCountryDto>? toDelete);
}
