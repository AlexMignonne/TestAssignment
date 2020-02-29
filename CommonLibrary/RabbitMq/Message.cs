using System;

namespace CommonLibrary.RabbitMq
{
    public abstract class Message<TExchange>
        where TExchange : RabbitExchange, new()
    {
        protected Message()
        {
            ExchangeName = new TExchange()
                .Name;

            MessageType = GetType()
                .Name;

            DateCreated = DateTime
                .UtcNow;
        }

        public string ExchangeName { get; }
        public string MessageType { get; }
        public DateTime DateCreated { get; }
    }
}
