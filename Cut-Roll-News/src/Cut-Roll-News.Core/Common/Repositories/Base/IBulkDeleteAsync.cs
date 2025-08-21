namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IBulkDeleteAsync<TEntity, TResult>
{
    public Task<TResult> BulkDeleteAsync(IEnumerable<TEntity> listToDelete);
}
