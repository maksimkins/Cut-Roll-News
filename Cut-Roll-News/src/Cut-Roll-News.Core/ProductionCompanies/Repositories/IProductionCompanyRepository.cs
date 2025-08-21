using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.ProductionCompanies.Dtos;
using Cut_Roll_News.Core.ProductionCompanies.Models;

namespace Cut_Roll_News.Core.ProductionCompanies.Repositores;

public interface IProductionCompanyRepository: ISearchAsync<ProductionCompanySearchRequest, PagedResult<ProductionCompany>>,
ICreateAsync<ProductionCompanyCreateDto, Guid?>, IDeleteByIdAsync<Guid, Guid?>, IUpdateAsync<ProductionCompanyUpdateDto, Guid?>,
IGetByIdAsync<Guid, ProductionCompany?>
{
    
}
