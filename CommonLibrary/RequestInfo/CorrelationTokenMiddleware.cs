using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CommonLibrary.RequestInfo
{
    public sealed class CorrelationTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationTokenMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task InvokeAsync(
            HttpContext context)
        {
            var token = context
                .Request
                .Headers["Correlation-Token"];

            RequestInfo
                .CorrelationToken = token;

            return _next(context);
        }
    }
}
