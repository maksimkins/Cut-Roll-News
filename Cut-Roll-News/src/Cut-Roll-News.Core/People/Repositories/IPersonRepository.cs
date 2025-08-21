using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.People.Dtos;
using Cut_Roll_News.Core.People.Models;

namespace Cut_Roll_News.Core.People.Repositories;

public interface IPersonRepository : ISearchAsync<PersonSearchRequest, PagedResult<Person>>, IGetByIdAsync<Guid, Person?>,
IUpdateAsync<PersonUpdateDto, Guid?>, IDeleteByIdAsync<Guid, Guid?>, ICreateAsync<PersonCreateDto, Guid?>
{
    Task<PagedResult<MovieSimplifiedDto>> GetFilmographyAsync(MovieSearchByPesonIdDto searchByPersonIdDto);
}
