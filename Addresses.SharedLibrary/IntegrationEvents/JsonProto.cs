using System.Text;
using CommonLibrary.RabbitMq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Addresses.SharedLibrary.IntegrationEvents
{
    public sealed class JsonProto
        : IRabbitProto
    {
        public byte[] MessageToBytes<TMessage, TExchange>(
            TMessage message)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new()
        {
            var json = JsonSerializer
                .Serialize(
                    message);

            return Encoding
                .UTF8
                .GetBytes(
                    json);
        }

        public TMessage BytesToMessage<TMessage, TExchange>(
            byte[] bytes)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new()
        {
            var json = Encoding
                .UTF8
                .GetString(bytes);

            return JsonConvert
                .DeserializeObject<TMessage>(json);
        }
    }
}
