using System.Threading.Tasks;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMq.Publisher
{
    public interface IRabbitPublisher
    {
        Task Publish<TMessage, TExchange>(
            TMessage message,
            string? routingKey = null,
            bool mandatory = false,
            IBasicProperties? properties = null)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new();
    }
}
