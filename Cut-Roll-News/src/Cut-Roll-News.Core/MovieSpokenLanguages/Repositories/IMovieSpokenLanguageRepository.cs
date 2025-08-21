namespace Cut_Roll_News.Core.MovieSpokenLanguages.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Interfaces;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.MovieSpokenLanguages.Dtos;
using Cut_Roll_News.Core.SpokenLanguages.Models;

public interface IMovieSpokenLanguageRepository : ICreateAsync<MovieSpokenLanguageDto, Guid?>, IDeleteAsync<MovieSpokenLanguageDto, Guid?>,
IDeleteRangeById<Guid, bool>, IBulkCreateAsync<MovieSpokenLanguageDto, bool>, IBulkDeleteAsync<MovieSpokenLanguageDto, bool>
{
    Task<IEnumerable<SpokenLanguage>> GetSpokenLanguagesByMovieIdAsync(Guid movieId);
    Task<bool> ExistsAsync(MovieSpokenLanguageDto dto);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesBySpokenLanguageIdAsync(MovieSearchBySpokenLanguageDto movieSearchByCountryDto);
}
