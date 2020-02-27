using System;

namespace Accounts.Application.Exceptions
{
    public sealed class ApplicationException
        : Exception
    {
        internal ApplicationException()
        {
        }

        internal ApplicationException(
            string message)
            : base(message)
        {
        }

        internal ApplicationException(
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
