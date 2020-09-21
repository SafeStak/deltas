using SafeStak.Deltas.WebApi.Exceptions;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SafeStak.Deltas.WebApi.AdaPools
{
    public interface IAdaPoolsApiClient
    {
        Task<PoolSummary> GetPoolSummaryAsync(string poolId, CancellationToken ct = default);
    }

    public class AdaPoolsApiClient : IAdaPoolsApiClient
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;
        private readonly PoolApiSettings _poolApiSettings;

        public AdaPoolsApiClient(HttpClient httpClient, PoolApiSettings poolApiSettings)
        {
            _httpClient = httpClient;
            _poolApiSettings = poolApiSettings ?? throw new ArgumentNullException(nameof(poolApiSettings));
        }

        public async Task<PoolSummary> GetPoolSummaryAsync(string poolId, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"/pools/{poolId}/summary.json", UriKind.Relative),
                Method = HttpMethod.Get
            };

            try
            {
                var response = await _httpClient.SendAsync(request, ct).ConfigureAwait(false);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new PoolApiResponseException($"Unsuccessful response from {request.RequestUri}", request, response);
                }

                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                if (responseStream == null || responseStream.Length == 0)
                {
                    throw new PoolApiResponseException($"Null or empty response from {request.RequestUri}", request);
                }

                return await JsonSerializer.DeserializeAsync<PoolSummary>(responseStream, JsonSerializerOptions).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                throw new PoolApiResponseException($"Failed to send request to {request.RequestUri}", request, e);
            }
            catch (JsonException e)
            {
                throw new PoolApiResponseException($"Failed to deserialize response from {request.RequestUri}", request, e);
            }
        }
    }
}
