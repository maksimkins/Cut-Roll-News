namespace Cut_Roll_News.Core.ProductionCompanies.Service;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.ProductionCompanies.Dtos;
using Cut_Roll_News.Core.ProductionCompanies.Models;

public interface IProductionCompanyService
{
    Task<PagedResult<ProductionCompany>> SearchProductionCompanyAsync(ProductionCompanySearchRequest? request);
    Task<Guid> CreateProductionCompanyAsync(ProductionCompanyCreateDto? dto);
    Task<Guid> DeleteProductionCompanyById(Guid? id);
    Task<Guid> UpdateProductionCompanyAsync(ProductionCompanyUpdateDto? dto);
    Task<ProductionCompany?> GetProductionCompanyByIdAsync(Guid? id);
}


