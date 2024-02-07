using Microsoft.Azure.ServiceBus;
using Queue.Interface;

namespace Queue.Queue
{
    public class QueueClientWrapper : IQueueClientWrapper
    {
        private readonly QueueClient _queueClient;

        public QueueClientWrapper(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task SendAsync(Message message)
        {
            await _queueClient.SendAsync(message);
        }

    }
}

