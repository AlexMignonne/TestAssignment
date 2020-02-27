using System.Threading.Tasks;
using CommonLibrary.RabbitMq.Declare;
using CommonLibrary.RabbitMq.Messages;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMq
{
    public interface IRabbitPublisher
    {
        Task Publish<TMessage, TExchange>(
            TMessage message,
            string routingKey = null,
            bool mandatory = false,
            IBasicProperties properties = null)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new();
    }
}
