namespace Cut_Roll_News.Core.MovieProductionCompanies.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Interfaces;
using Cut_Roll_News.Core.MovieProductionCompanies.Dtos;
using Cut_Roll_News.Core.Movies.Dtos;
using Cut_Roll_News.Core.ProductionCompanies.Models;

public interface IMovieProductionCompanyRepository : ICreateAsync<MovieProductionCompanyDto, Guid?>, IDeleteAsync<MovieProductionCompanyDto, Guid?>,
IDeleteRangeById<Guid, bool>, IBulkCreateAsync<MovieProductionCompanyDto, bool>, IBulkDeleteAsync<MovieProductionCompanyDto, bool>
{
    Task<IEnumerable<ProductionCompany>> GetCompaniesByMovieIdAsync(Guid movieId);
    Task<PagedResult<MovieSimplifiedDto>> GetMoviesByCompanyIdAsync(MovieSearchByCompanyDto movieSearchByCompanyDto);
    Task<bool> ExistsAsync(MovieProductionCompanyDto dto);
}
