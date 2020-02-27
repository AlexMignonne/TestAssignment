using System;

namespace CommonLibrary.RabbitMq
{
    public sealed class RabbitConfig
    {
        public string? Uri { get; set; }

        public bool IsConfigured()
        {
            return System.Uri
                .TryCreate(
                    Uri,
                    UriKind.Absolute,
                    out _);
        }
    }
}
