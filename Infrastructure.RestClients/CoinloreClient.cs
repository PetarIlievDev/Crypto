namespace Infrastructure.RestClients
{
    using System;
    using System.Net.Http.Json;
    using Infrastructure.RestClients.Interfaces;
    using Infrastructure.RestClients.Models;
    using Microsoft.AspNetCore.WebUtilities;

    public class CoinloreClient : ICoinloreClient
    {
        private readonly string COINLORE_API_PATH = "https://api.coinlore.net/";
        private readonly string TICKERS_PATH = "api/tickers/";

        public CoinloreClient()
        {
        }

        public async Task<List<Ticker>> GetCoinsInfoAsync(Dictionary<string, string?> paramValues, CancellationToken ct)
        {

            var request = ComposeGetRequest(TICKERS_PATH, paramValues);
            using var response = await PerformGetRequestAsync(request, ct);
            var tickers = await response.Content.ReadFromJsonAsync<BaseReponseModel<Ticker>>(ct);
            return tickers?.Data ?? [];
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

        private static async Task<HttpResponseMessage> PerformGetRequestAsync(Uri uri, CancellationToken ct)
        {

            using var httpClient = new HttpClient() { BaseAddress = uri};
            try
            {
                var response = await httpClient.GetAsync(uri, ct);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"REST Service connect error: {ex.Message}");
            }
        }
    }
}
