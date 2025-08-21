namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface IDeleteRangeById<TId, TReturn>
{
    Task<TReturn> DeleteRangeById(TId id);
}
