namespace Cut_Roll_News.Core.MovieProductionCountries.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.Countries.Models;
using Cut_Roll_News.Core.MovieProductionCountries.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.Movies.Models;


public interface IMovieProductionCountryRepository : ICreateAsync<MovieProductionCountryDto, Guid?>, IDeleteAsync<MovieProductionCountryDto, Guid?>,
IDeleteRangeById<Guid, bool>, IBulkCreateAsync<MovieProductionCountryDto, bool>, IBulkDeleteAsync<MovieProductionCountryDto, bool>
{
    Task<bool> ExistsAsync(MovieProductionCountryDto dto);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByCountryIdAsync(MovieSearchByCountryDto movieSearchByCountryDto);
    Task<IEnumerable<Country>> GetCountriesByMovieIdAsync(Guid movieId);
}
