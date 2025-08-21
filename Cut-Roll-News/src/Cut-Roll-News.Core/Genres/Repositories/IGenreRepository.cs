namespace Cut_Roll_News.Core.Genres.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Interfaces;
using Cut_Roll_News.Core.Genres.Dtos;
using Cut_Roll_News.Core.Genres.Models;

public interface IGenreRepository : IDeleteByIdAsync<Guid, Guid?>, ICreateAsync<GenreCreateDto, Guid?>, IGetByIdAsync<Guid, Genre?>,
ISearchAsync<GenreSearchDto, PagedResult<Genre>>
{
    public Task<PagedResult<Genre>> GetAllAsync(GenrePaginationDto dto);
    public Task<bool> ExistsAsync(Guid id);
    public Task<bool> ExistsByNameAsync(string name);
}
