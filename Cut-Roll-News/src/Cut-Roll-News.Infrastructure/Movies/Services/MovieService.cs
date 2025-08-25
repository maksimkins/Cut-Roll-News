namespace Cut_Roll_News.Infrastructure.Movies.Services;

using Cut_Roll_News.Core.Casts.Repositories;
using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Crews.Repositories;
using Cut_Roll_News.Core.MovieGenres.Repositories;
using Cut_Roll_News.Core.MovieImages.Repositories;
using Cut_Roll_News.Core.MovieKeywords.Repositories;
using Cut_Roll_News.Core.MovieOriginCountries.Repository;
using Cut_Roll_News.Core.MovieProductionCompanies.Repositories;
using Cut_Roll_News.Core.MovieProductionCountries.Repositories;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.Movies.Models;
using Cut_Roll_News.Core.Movies.Repositories;
using Cut_Roll_News.Core.Movies.Service;
using Cut_Roll_News.Core.MovieSpokenLanguages.Repositories;
using Cut_Roll_News.Core.MovieVideos.Repositories;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastRepository _castRepository;
    private readonly ICrewRepository _crewRepository;
    private readonly IMovieGenreRepository _movieGenreRepository;
    private readonly IMovieKeywordRepository _movieKeywordRepository;
    private readonly IMovieProductionCompanyRepository _movieProductionCompanyRepository;
    private readonly IMovieProductionCountryRepository _movieProductionCountryRepository;
    private readonly IMovieOriginCountryRepository _movieOriginCountryRepository;
    private readonly IMovieSpokenLanguageRepository _movieSpokenLanguageRepository;
    private readonly IMovieVideoRepository _movieVideoRepository;
    private readonly IMovieImageRepository _movieImageRepository;

    public MovieService(
        IMovieRepository movieRepository,
        ICastRepository castRepository,
        ICrewRepository crewRepository,
        IMovieGenreRepository movieGenreRepository,
        IMovieKeywordRepository movieKeywordRepository,
        IMovieProductionCompanyRepository movieProductionCompanyRepository,
        IMovieProductionCountryRepository movieProductionCountryRepository,
        IMovieOriginCountryRepository movieOriginCountryRepository,
        IMovieSpokenLanguageRepository movieSpokenLanguageRepository,
        IMovieVideoRepository movieVideoRepository,
        IMovieImageRepository movieImageRepository)
    {
        _movieRepository = movieRepository;
        _castRepository = castRepository;
        _crewRepository = crewRepository;
        _movieGenreRepository = movieGenreRepository;
        _movieKeywordRepository = movieKeywordRepository;
        _movieProductionCompanyRepository = movieProductionCompanyRepository;
        _movieProductionCountryRepository = movieProductionCountryRepository;
        _movieOriginCountryRepository = movieOriginCountryRepository;
        _movieSpokenLanguageRepository = movieSpokenLanguageRepository;
        _movieVideoRepository = movieVideoRepository;
        _movieImageRepository = movieImageRepository;
    }
    public async Task<int> CountMoviesAsync()
    {
        return await _movieRepository.CountAsync();
    }

    public Task<Guid> CreateMovieAsync(MovieCreateDto? dto)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteMovieByIdAsync(Guid? id)
    {
        throw new NotImplementedException();
    }

    public async Task<Movie?> GetMovieByIdAsync(Guid? id)
    {
        if (id == null || id == Guid.Empty)
            throw new ArgumentNullException($"missing {nameof(id)}");

        return await _movieRepository.GetByIdAsync(id.Value);
    }

    public async Task<PagedResult<MovieSimplifiedDto>> SearchMovieAsync(MovieSearchRequest? dto)
    {
        if (dto == null)
            throw new ArgumentNullException($"missing {nameof(dto)}");

        return await _movieRepository.SearchAsync(dto);
    }

    public async Task<Guid> UpdateMovieAsync(MovieUpdateDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException($"missing {nameof(dto)}");

        return await _movieRepository.UpdateAsync(dto)
            ?? throw new InvalidOperationException("Movie update failed.");
    }
}
