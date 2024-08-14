namespace WebApi.Services
{
    using System.Globalization;
    using System.Linq;
    using Infrastructure.RestClients.Interfaces;
    using WebApi.Services.Interfaces;
    using WebApi.Services.Models.ChangeFromInitialBuy;

    public class CoinloreService(ICoinloreClient coinloreClient, ILogService logService) : ICoinloreService
    {
        private readonly ICoinloreClient _coinloreClient = coinloreClient;
        private readonly ILogService _logService = logService;

        public async Task<CalculatedOverallChangeFromInitialBuy> CalculateChangeFromInitialBuyAsync(InitialBuyData initialBuyData, CancellationToken ct)
        {
            _logService.LogToFile($"Guid: {initialBuyData.Guid}, LogMessage: User requests is for {initialBuyData.InitialBuyDataFromRequestList.Count} crypto coins.");

            if (initialBuyData.InitialBuyDataFromRequestList.Count == 0)
            {
                throw new Exception($"Guid: {initialBuyData.Guid}, LogMessage: No data received from user");
            }

            var cryptoCyrencySymbol = initialBuyData.InitialBuyDataFromRequestList.Select(x => x.CryptoCurrencySymbol.ToUpperInvariant());
            var coinsInfoList = await _coinloreClient.GetCoinsInfoAsync(null, ct);

            if(coinsInfoList is null || coinsInfoList.Count == 0)
            {
                throw new Exception($"Guid: {initialBuyData.Guid}, LogMessage: No data received from Coinlore API");
            }

            if (!cryptoCyrencySymbol.All(value => coinsInfoList.Select(x=>x.Symbol.ToUpperInvariant()).Contains(value)))
            {
                do
                {
                    var parameterValues = new Dictionary<string, string?>
                    {
                        { "start", $"{coinsInfoList.Count}" },
                        { "limit", "100" }
                    };
                    coinsInfoList.AddRange(await _coinloreClient.GetCoinsInfoAsync(parameterValues, ct));
                } while (!cryptoCyrencySymbol.All(value => coinsInfoList.Select(x => x.Symbol.ToUpperInvariant()).Contains(value)));
            }

            _logService.LogToFile($"Guid: {initialBuyData.Guid}, LogMessage: All needed data recieved on {coinsInfoList.Count / 100} pages.");

            var result = new CalculatedOverallChangeFromInitialBuy();

            foreach (var item in initialBuyData.InitialBuyDataFromRequestList)
            {
                var actualPrice = decimal.Parse(coinsInfoList.Where(x => x.Symbol == item.CryptoCurrencySymbol).Select(x => x.Price_usd).FirstOrDefault(), CultureInfo.InvariantCulture);
                result.CalculatedChangeFromInitialBuyList.Add(new CalculatedChangeFromInitialBuy
                {
                    CryptoCurrency = item.CryptoCurrencySymbol,
                    CurrentPriceInUsd = actualPrice,
                    ChangeInPercentage = Math.Round(((actualPrice - item.InitialBuyPrice) / item.InitialBuyPrice) * 100, 2),
                    OverallPerCurrencyInUsd = Math.Round(item.NumberOfCoins * (actualPrice - item.InitialBuyPrice), 4)
                });
            }

            result.OverallChangeInPercentage = Math.Round(result.CalculatedChangeFromInitialBuyList.Sum(x => x.ChangeInPercentage), 2);
            result.OverallChangeInPriceUsd = Math.Round(result.CalculatedChangeFromInitialBuyList.Sum(x => x.OverallPerCurrencyInUsd), 4);

            return result;
        }
    }
}
