namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IUpdateAsync<TEntity, TReturn> 
{
    Task<TReturn> UpdateAsync(TEntity entity);
}
