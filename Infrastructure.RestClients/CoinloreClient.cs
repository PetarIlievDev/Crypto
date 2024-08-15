namespace Infrastructure.RestClients
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using Infrastructure.RestClients.Interfaces;
    using Infrastructure.RestClients.Models;
    using Microsoft.AspNetCore.WebUtilities;

    public class CoinloreClient : ICoinloreClient
    {
        private readonly HttpClient _httpClient;
        private readonly string COINLORE_API_PATH = "https://api.coinlore.net/";
        private readonly string TICKERS_PATH = "api/tickers/";

        public CoinloreClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TickersData> GetCoinsInfoAsync(Dictionary<string, string?> paramValues, CancellationToken ct)
        {
            var request = ComposeGetRequest(TICKERS_PATH, paramValues);
            using var response = await PerformGetRequestAsync(request, ct);
            var tickers = await response.Content.ReadFromJsonAsync<TickersData>(ct);
            return tickers ?? new TickersData();
        }

        private Uri ComposeGetRequest(string resourcePath, Dictionary<string, string?> paramValues)
        {
            var builder = new UriBuilder(COINLORE_API_PATH)
            {
                Path = resourcePath
            };

            if (paramValues != null && paramValues.Count > 0)
            {
                builder.Query = QueryHelpers.AddQueryString(string.Empty, paramValues);
            }

            return builder.Uri;
        }

        private async Task<HttpResponseMessage> PerformGetRequestAsync(Uri uri, CancellationToken ct)
        {            
            try
            {
                var response = await _httpClient.GetAsync(uri, ct);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"REST Service connect error: {ex.Message}");
            }
        }
    }
}
