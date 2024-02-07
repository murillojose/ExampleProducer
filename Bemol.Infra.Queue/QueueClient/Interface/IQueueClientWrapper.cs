using Microsoft.Azure.ServiceBus;

namespace Queue.Interface
{
    public interface IQueueClientWrapper
    {
        Task SendAsync(Message message);
    }
}
