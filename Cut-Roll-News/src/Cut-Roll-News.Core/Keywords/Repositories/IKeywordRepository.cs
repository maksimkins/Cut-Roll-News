namespace Cut_Roll_News.Core.Keywords.Repositories;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.Keywords.Dtos;
using Cut_Roll_News.Core.Keywords.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


public interface IKeywordRepository : IDeleteByIdAsync<Guid, Guid?>, ICreateAsync<KeywordCreateDto, Guid?>,
ISearchAsync<KeywordSearchDto, PagedResult<Keyword>>, IGetByIdAsync<Guid, Keyword?>
{
    Task<PagedResult<Keyword>> GetAllAsync(KeywordPaginationDto dto);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
}
