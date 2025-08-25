using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Genres.Dtos;
using Cut_Roll_News.Core.Genres.Models;
using Cut_Roll_News.Core.MovieGenres.Dtos;
using Cut_Roll_News.Core.MovieGenres.Models;
using Cut_Roll_News.Core.MovieGenres.Repositories;
using Cut_Roll_News.Core.MovieImages.Enums;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.Movies.Models;
using Cut_Roll_News.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_News.Infrastructure.MovieGenres.Repositories;

public class MovieGenreEfCoreRepository : IMovieGenreRepository
{
    private readonly NewsDbContext _dbContext;
    public MovieGenreEfCoreRepository(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> BulkCreateAsync(IEnumerable<MovieGenreDto> listToCreate)
    {
        var newList = listToCreate.Select(toCreate => new MovieGenre
        {
            MovieId = toCreate.MovieId,
            GenreId = toCreate.GenreId,
        });
        
        await _dbContext.MovieGenres.AddRangeAsync(newList);
        var res = await _dbContext.SaveChangesAsync();

        return res > 0;
    }

    public async Task<bool> BulkDeleteAsync(IEnumerable<MovieGenreDto> listToDelete)
    {
        foreach (var item in listToDelete)
        {
            var movieGenre = await _dbContext.MovieGenres.FirstOrDefaultAsync(c =>
                c.MovieId == item.MovieId && c.GenreId == item.GenreId);

            if (movieGenre != null)
            {
                _dbContext.MovieGenres.Remove(movieGenre);
            }
        }

        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Guid?> CreateAsync(MovieGenreDto entity)
    {
        var movieGenre = new MovieGenre
        {
            MovieId = entity.MovieId,
            GenreId = entity.GenreId,
        };

        await _dbContext.MovieGenres.AddAsync(movieGenre);
        var res = await _dbContext.SaveChangesAsync();

        return res > 0 ? entity.MovieId : null;
    }

    public async Task<Guid?> DeleteAsync(MovieGenreDto dto)
    {
        var toDelete = await _dbContext.MovieGenres.FirstOrDefaultAsync(g => g.MovieId == dto.MovieId && g.GenreId == dto.GenreId);
        if (toDelete != null)
            _dbContext.MovieGenres.Remove(toDelete);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0 ? dto.MovieId : null;
    }

    public async Task<bool> DeleteRangeById(Guid movieId)
    {
        var toDeletes = _dbContext.MovieGenres.Where(g => g.MovieId == movieId);
        _dbContext.MovieGenres.RemoveRange(toDeletes);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> ExistsAsync(MovieGenreDto dto)
    {
        return await _dbContext.MovieGenres.AnyAsync(g => g.MovieId == dto.MovieId && g.GenreId == dto.GenreId);
    }

    public async Task<IEnumerable<Genre>> GetGenresByMovieIdAsync(Guid movieId)
    {
        return await _dbContext.MovieGenres.Where(g => g.MovieId == movieId).
            Include(g => g.Genre).Select(g => g.Genre).ToListAsync();
    }

    public async Task<PagedResult<MovieSimplifiedDto>> GetMoviesByGenreIdAsync(MovieSearchByGenreDto searchDto)
    {
        var query = _dbContext.Movies
            .Include(m => m.Images)
            .Include(m => m.MovieGenres)
            .ThenInclude(mg => mg.Genre)
            .AsQueryable();

        if (searchDto.GenreId.HasValue)
        {
            query = query.Where(m => m.MovieGenres.Any(mg => mg.GenreId == searchDto.GenreId.Value));
        }

        else if (!string.IsNullOrWhiteSpace(searchDto.Name))
        {
            var name = $"%{searchDto.Name.Trim()}%";
            query = query.Where(m => m.MovieGenres.Any(k => EF.Functions.ILike(k.Genre.Name, name)));
        }

        if (searchDto.PageNumber < 1) searchDto.PageNumber = 1;
        if (searchDto.PageSize < 1) searchDto.PageSize = 10;

        var totalCount = await query.CountAsync();

        query = query.
            Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
            .Take(searchDto.PageSize);
        
        var result = await query.ToListAsync();

        return new PagedResult<MovieSimplifiedDto>()
        {
            Data = result.Select(m => new MovieSimplifiedDto
            {
                MovieId = m.Id,
                Title = m.Title,
                Poster = m.Images?.FirstOrDefault(i => i.Type == ImageTypes.poster.ToString()),
            }).ToList(),
            TotalCount = totalCount,
            Page = searchDto.PageNumber,
            PageSize = searchDto.PageSize
        };
    }
}
