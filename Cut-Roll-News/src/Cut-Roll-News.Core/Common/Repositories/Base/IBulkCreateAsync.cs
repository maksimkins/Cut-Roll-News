namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IBulkCreateAsync<TEntity, TResult>
{
    public Task<TResult> BulkCreateAsync(IEnumerable<TEntity> listToCreate);
}
