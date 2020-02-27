using CommonLibrary.RabbitMq.Declare;
using CommonLibrary.RabbitMq.Messages;

namespace CommonLibrary.RabbitMq
{
    public interface IRabbitProto
    {
        byte[] MessageToBytes<TMessage, TExchange>(
            TMessage message)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new();

        TMessage BytesToMessage<TMessage, TExchange>(
            byte[] bytes)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new();
    }
}
