namespace Cut_Roll_News.Core.Common.Repositories.Base;

public interface ISearchAsync<TRequest, TResponse>
{
    Task<TResponse> SearchAsync(TRequest request);
}

