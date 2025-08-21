namespace Cut_Roll_News.Core.Movies.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Interfaces;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.Movies.Models;


public interface IMovieRepository : ISearchAsync<MovieSearchRequest, PagedResult<MovieSimplifiedDto>>, IGetByIdAsync<Guid, Movie?>,
IUpdateAsync<MovieUpdateDto, Guid?>, IDeleteByIdAsync<Guid, Guid?>, ICreateAsync<MovieCreateDto, Guid?>, ICountAsync
{
}