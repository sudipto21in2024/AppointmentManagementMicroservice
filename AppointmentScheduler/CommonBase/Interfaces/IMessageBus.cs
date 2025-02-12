using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace CommonBase.Interfaces
{
   
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message) where T : class;
        Task SubscribeAsync<T>(string subscriptionId, Func<T, CancellationToken, Task> handler) where T : class;
    }
}
