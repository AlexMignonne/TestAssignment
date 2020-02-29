using System;

namespace CommonLibrary.RabbitMq.RabbitMqExceptions
{
    public sealed class RabbitMqException
        : Exception
    {
        internal RabbitMqException()
        {
        }

        internal RabbitMqException(
            string message)
            : base(message)
        {
        }

        internal RabbitMqException(
            string message,
            Exception innerException
        )
            : base(
                message,
                innerException)
        {
        }
    }
}
