using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.Address;
using Newtonsoft.Json;

namespace Accounts.Infrastructure.HttpClients
{
    public sealed class AddressesHttpClient
    {
        private readonly HttpClient _httpClient;

        public AddressesHttpClient(
            HttpClient httpClient)
        {
            httpClient
                .DefaultRequestVersion = new Version(
                2,
                0);

            httpClient
                .BaseAddress = new Uri("http://addresses.service");

            httpClient
                .DefaultRequestHeaders
                .Add(
                    "Accept",
                    "application/json");

            httpClient
                .DefaultRequestHeaders
                .Add(
                    "User-Agent",
                    "TestAssignment.Accounts.Infrastructure");

            _httpClient = httpClient;
        }

        public async Task<AddressDomain?> GetByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            _httpClient
                .DefaultRequestHeaders
                .Add(
                    "correlation_token",
                    correlationToken);

            var response = await _httpClient
                .GetAsync(
                    $"Addresses/province/{id}",
                    token);

            if (response.ToString().StartsWith('5'))
                response
                    .EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response
                .Content
                .ReadAsStringAsync();

            return JsonConvert
                .DeserializeObject<AddressDomain>(json);
        }

        public async Task<bool> IsExistProvinceById(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            _httpClient
                .DefaultRequestHeaders
                .Add(
                    "correlation_token",
                    correlationToken);

            var response = await _httpClient
                .GetAsync(
                    $"Provinces/is_exist/{id}",
                    token);

            if (response.ToString().StartsWith('5'))
                response
                    .EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
