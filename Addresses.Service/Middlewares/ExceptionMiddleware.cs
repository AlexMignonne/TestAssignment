using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Addresses.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ApplicationException = Addresses.Application.Exceptions.ApplicationException;

namespace Addresses.Service.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;

            _logger = loggerFactory
                .CreateLogger<ExceptionMiddleware>();
        }

        [SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "<Pending>")]
        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(
                    context,
                    e);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            if (exception is DomainException ||
                exception is ApplicationException)
            {
                _logger
                    .LogInformation(
                        "{0}: {1}",
                        exception
                            .InnerException
                            ?.Source,
                        exception
                            .InnerException
                            ?.Message);

                context
                    .Response
                    .StatusCode = (int) HttpStatusCode.BadRequest;

                context
                    .Response
                    .ContentType = "text/plain";

                await context
                    .Response
                    .WriteAsync(
                        exception
                            .Message);
            }
            else
            {
                _logger
                    .LogError(
                        exception,
                        "Message: {0}",
                        exception.Message);

                context
                    .Response
                    .StatusCode = (int) HttpStatusCode.InternalServerError;

                context
                    .Response
                    .ContentType = "text/plain";

                await context
                    .Response
                    .WriteAsync("Internal server error");
            }
        }
    }
}
