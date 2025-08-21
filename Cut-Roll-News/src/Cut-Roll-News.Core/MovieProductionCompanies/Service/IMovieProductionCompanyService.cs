namespace Cut_Roll_News.Core.MovieProductionCompanies.Service;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.MovieProductionCompanies.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.ProductionCompanies.Models;

public interface IMovieProductionCompanyService
{
    Task<Guid> CreateMovieProductionCompanyAsync(MovieProductionCompanyDto? dto);
    Task<Guid> DeleteMovieProductionCompanyAsync(MovieProductionCompanyDto? dto);
    Task<bool> DeleteMovieProductionCompanyRangeByMovieIdAsync(Guid? movieId);
    Task<IEnumerable<ProductionCompany>> GetCompaniesByMovieIdAsync(Guid? movieId);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByCompanyIdAsync(MovieSearchByCompanyDto? movieSearchByCompanyDto);
    Task<bool> BulkCreateMovieProductionCompanyAsync(IEnumerable<MovieProductionCompanyDto>? toCreate);
    Task<bool> BulkDeleteMovieProductionCompanyAsync(IEnumerable<MovieProductionCompanyDto>? toDelete);
}
