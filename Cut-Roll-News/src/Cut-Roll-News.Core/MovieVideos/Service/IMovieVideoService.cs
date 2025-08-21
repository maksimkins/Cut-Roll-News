namespace Cut_Roll_News.Core.MovieVideos.Service;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.MovieVideos.Dtos;
using Cut_Roll_News.Core.MovieVideos.Models;

public interface IMovieVideoService
{
    Task<Guid> CreateMovieVideoCreateAsync(MovieVideoCreateDto? dto);
    Task<Guid> DeleteMovieVideoByIdAsync(Guid? id);
    Task<bool> DeleteMovieVideoRangeByMovieId(Guid? movieId);
    Task<Guid> UpdateMovieVideoAsync(MovieVideoUpdateDto? dto);
    Task<IEnumerable<MovieVideo>> GetVideosByMovieIdAsync(Guid? movieId);
    Task<IEnumerable<MovieVideo>> GetVideosByTypeAsync(MovieVideoSearchDto? dto);
}
