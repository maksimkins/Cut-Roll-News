
namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IGetByIdAsync<TEntity, TId>
{
    Task<TEntity?> GetByIdAsync(TId id);
}