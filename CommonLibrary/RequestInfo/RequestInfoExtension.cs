using Microsoft.AspNetCore.Builder;

namespace CommonLibrary.RequestInfo
{
    public static class RequestInfoExtension
    {
        public static IApplicationBuilder UseRequestInfo(
            this IApplicationBuilder app)
        {
            app
                .UseMiddleware<CorrelationTokenMiddleware>()
                .UseMiddleware<RemoteIpAddressMiddleware>();

            return app;
        }
    }
}
