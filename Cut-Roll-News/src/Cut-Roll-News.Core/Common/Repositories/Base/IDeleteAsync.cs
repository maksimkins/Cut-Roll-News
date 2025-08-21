namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IDeleteAsync<TEntity, TReturn>
{
    Task<TReturn> DeleteAsync(TEntity entity);
}
