using System;
using System.Net;
using System.Threading;

namespace CommonLibrary.RequestInfo
{
    public sealed class RequestInfo
    {
        private static readonly AsyncLocal<string> CorrelationTokenAsyncLocal =
            new AsyncLocal<string>();

        private static readonly AsyncLocal<IPAddress?> IpAddressAsyncLocal
            = new AsyncLocal<IPAddress?>();

        public static string CorrelationToken
        {
            get => CorrelationTokenAsyncLocal.Value ??
                   Guid
                       .NewGuid()
                       .ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = Guid
                        .NewGuid()
                        .ToString();

                CorrelationTokenAsyncLocal.Value = value;
            }
        }

        public static IPAddress? RemoteIp
        {
            get => IpAddressAsyncLocal.Value;
            set => IpAddressAsyncLocal.Value = value;
        }
    }
}
