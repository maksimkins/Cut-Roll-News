namespace Cut_Roll_News.Core.MovieKeywords.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Interfaces;
using Cut_Roll_News.Core.Keywords.Models;
using Cut_Roll_News.Core.MovieKeywords.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;

public interface IMovieKeywordRepository : ICreateAsync<MovieKeywordDto, Guid?>, IDeleteAsync<MovieKeywordDto, Guid?>, IDeleteRangeById<Guid, bool>, 
IBulkCreateAsync<MovieKeywordDto, bool>, IBulkDeleteAsync<MovieKeywordDto, bool>
{
    Task<IEnumerable<Keyword>> GetKeywordsByMovieIdAsync(Guid movieId);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByKeywordIdAsync(MovieSearchByKeywordDto searchDto);
    Task<bool> ExistsAsync(MovieKeywordDto dto);
}
