using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace CommonLibrary.RabbitMq.Handler
{
    public interface IRabbitHandler<in TMessage, TExchange>
        where TMessage : Message<TExchange>
        where TExchange : RabbitExchange, new()
    {
        Task Receive(
            TMessage message,
            BasicDeliverEventArgs args);
    }
}
