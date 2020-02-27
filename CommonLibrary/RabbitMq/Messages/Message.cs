using System;
using CommonLibrary.RabbitMq.Declare;

namespace CommonLibrary.RabbitMq.Messages
{
    public abstract class Message<TExchange>
        where TExchange : RabbitExchange, new()
    {
        protected Message(
            string correlationIToken)
        {
            CorrelationToken = correlationIToken;

            ExchangeName = new TExchange()
                .Name;

            MessageType = GetType()
                .Name;

            DateCreated = DateTime
                .UtcNow;
        }

        public string CorrelationToken { get; }
        public string ExchangeName { get; }
        public string MessageType { get; }
        public DateTime DateCreated { get; }
    }
}
