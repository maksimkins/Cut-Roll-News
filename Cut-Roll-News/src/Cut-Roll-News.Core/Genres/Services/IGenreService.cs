namespace Cut_Roll_News.Core.Genres.Services;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Genres.Dtos;
using Cut_Roll_News.Core.Genres.Models;

public interface IGenreService
{
    public Task<Guid> DeleteGenreByIdAsync(Guid? id);
    public Task<Guid> CreateGenreAsync(GenreCreateDto? dto);
    public Task<Genre?> GetGenreByIdAsync(Guid? id);
    public Task<PagedResult<Genre>> GetAllGenresAsync(GenrePaginationDto? dto);
    public Task<PagedResult<Genre>> SearchGenreAsync(GenreSearchDto? dto);
}


