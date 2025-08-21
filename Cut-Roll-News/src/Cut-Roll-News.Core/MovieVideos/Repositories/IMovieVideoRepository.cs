namespace Cut_Roll_News.Core.MovieVideos.Repositories;

using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.MovieVideos.Dtos;
using Cut_Roll_News.Core.MovieVideos.Models;

public interface IMovieVideoRepository : ICreateAsync<MovieVideoCreateDto, Guid?>, IDeleteByIdAsync<Guid, Guid?>,
IDeleteRangeById<Guid, bool>, IUpdateAsync<MovieVideoUpdateDto, Guid?>
{
    Task<IEnumerable<MovieVideo>> GetVideosByMovieIdAsync(Guid movieId);
    Task<IEnumerable<MovieVideo>> GetVideosByTypeAsync(MovieVideoSearchDto dto);
}
