

namespace Cut_Roll_News.Core.Common.Services;
public interface IMessageBrokerService
{
    public Task PushAsync<T>(string destination, T obj);
}