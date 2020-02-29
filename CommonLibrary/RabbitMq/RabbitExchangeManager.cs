using System.Collections.Concurrent;

namespace CommonLibrary.RabbitMq
{
    internal static class RabbitExchangeManager
    {
        private static readonly ConcurrentDictionary<string, RabbitExchange> Exchanges
            = new ConcurrentDictionary<string, RabbitExchange>();

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
                .TryRemove(
                    name,
                    out _);
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
