namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IGetByIdAsync<TId, TReturnEntity>
{
    Task<TReturnEntity> GetByIdAsync(TId id);
}
