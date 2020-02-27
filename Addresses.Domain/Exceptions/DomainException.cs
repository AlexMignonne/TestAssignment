using System;

namespace Addresses.Domain.Exceptions
{
    public sealed class DomainException
        : Exception
    {
        internal DomainException()
        {
        }

        internal DomainException(
            string message)
            : base(message)
        {
        }

        internal DomainException(
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
