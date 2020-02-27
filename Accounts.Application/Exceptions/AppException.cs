using System;

namespace Accounts.Application.Exceptions
{
    public sealed class AppException
        : Exception
    {
        internal AppException()
        {
        }

        internal AppException(
            string message)
            : base(message)
        {
        }

        internal AppException(
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
