using System.Collections.Generic;

namespace CommonLibrary.RabbitMq.Declare
{
    internal static class RabbitExchangeDeclare
    {
        private static readonly Dictionary<string, RabbitExchange> Exchanges
            = new Dictionary<string, RabbitExchange>();

        internal static bool Add(
            RabbitExchange exchange)
        {
            if (string.IsNullOrWhiteSpace(
                exchange.Name))
                return false;

            return Exchanges
                .TryAdd(
                    exchange.Name,
                    exchange);
        }

        internal static bool Remove(
            string name)
        {
            if (string.IsNullOrWhiteSpace(
                name))
                return false;

            return Exchanges
                       .ContainsKey(name) &&
                   Exchanges
                       .Remove(
                           name);
        }

        internal static RabbitExchange? Get(
            string name)
        {
            if (string.IsNullOrWhiteSpace(
                name))
                return null;

            return Exchanges
                .TryGetValue(
                    name,
                    out var exchange)
                ? exchange
                : null;
        }
    }
}
