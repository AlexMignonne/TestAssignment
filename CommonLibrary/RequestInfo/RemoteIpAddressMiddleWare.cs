using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CommonLibrary.RequestInfo
{
    public sealed class RemoteIpAddressMiddleware
    {
        private readonly RequestDelegate _next;

        public RemoteIpAddressMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task InvokeAsync(
            HttpContext context)
        {
            var ipAddress = context
                .Request
                .HttpContext
                .Connection
                .RemoteIpAddress;

            RequestInfo
                .RemoteIp = ipAddress;

            return _next(context);
        }
    }
}
