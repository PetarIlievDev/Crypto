namespace WebApi.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.RestClients.Interfaces;
    using Infrastructure.RestClients.Models;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using NSubstitute.ClearExtensions;
    using WebApi.Services.Interfaces;
    using WebApi.Services.Models.ChangeFromInitialBuy;

    public class CoinloreServiceTests
    {
        private readonly ICoinloreClient _coinloreClientMock;
        private readonly ILogService _logServiceMock;
        private readonly ILogService _logService;
        private readonly ILogger<LogService> _loggerMock;
        private readonly CoinloreService _coinloreService;
        private readonly CoinloreService _coinloreServiceForLogger;

        public CoinloreServiceTests()
        {
            _coinloreClientMock = Substitute.For<ICoinloreClient>();
            _logServiceMock = Substitute.For<ILogService>();
            _loggerMock = Substitute.For<ILogger<LogService>>();
            _logService = new LogService(_loggerMock);
            _coinloreService = new CoinloreService(_coinloreClientMock, _logServiceMock);
            _coinloreServiceForLogger = new CoinloreService(_coinloreClientMock, _logService);
        }

        [TearDown]
        public void TearDown()
        {
            _coinloreClientMock.ClearSubstitute();
        }

        [Test]
        public void CalculateChangeFromInitialBuyAsyncLogWhenZeroCoins()
        {
            InitialBuyData initialBuyData = new()
            {
                Guid = Guid.NewGuid().ToString(),
            };
            var ex = Assert.ThrowsAsync<Exception>(() => _coinloreServiceForLogger.CalculateChangeFromInitialBuyAsync(initialBuyData, CancellationToken.None));

            _loggerMock.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Equals($"Guid: {initialBuyData.Guid}, LogMessage: User requests is for 0 crypto coins.")),
                null,
                Arg.Any<Func<object, Exception?, string>>()
            );
        }

        [Test]
        public async Task CalculateChangeFromInitialBuyAsyncLogWhenAllDataRecieved()
        {
            InitialBuyData initialBuyData = new()
            {
                Guid = Guid.NewGuid().ToString(),
            };
            initialBuyData.InitialBuyDataFromRequestList.Add(new InitialBuyDataFromRequest() { InitialBuyPrice = 10, CryptoCurrencySymbol = "ETH", NumberOfCoins = 123.14M });

            var tickersData = new TickersData()
            {
                Data =
                [
                    new Ticker(){
                        Id = "90",
                        Symbol = "BTC",
                        Name = "Bitcoin",
                        Nameid = "bitcoin",
                        Rank = 1,
                        Price_usd = "59119.49",
                        Percent_change_24h = "-2.53",
                        Percent_change_1h = "0.44",
                        Percent_change_7d = "3.66",
                        Price_btc = "1.00",
                        Market_cap_usd = "1166807656639.80",
                        Volume24 = 29458301782.254963,
                        Volume24a = 31011564057.907154,
                        Csupply = "19736430.00",
                        Tsupply = "19736430",
                        Msupply = "21000000" 
                    }
                ],
                TickerInfo = new TickerInfo()
                {
                    CoinsNum = 1,
                    Time = 1635730400
                }
            };

            _coinloreClientMock.GetCoinsInfoAsync(Arg.Any<Dictionary<string, string?>>(), Arg.Any<CancellationToken>()).Returns(tickersData);
            await _coinloreServiceForLogger.CalculateChangeFromInitialBuyAsync(initialBuyData, CancellationToken.None);

            _loggerMock.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Equals($"Guid: {initialBuyData.Guid}, LogMessage: All needed data recieved on {tickersData.Data.Count / 100} pages.")),
                null,
                Arg.Any<Func<object, Exception?, string>>()
            );
        }

        [Test]
        public void CalculateChangeFromInitialBuyAsyncThrowWhenZeroCoins()
        {
            InitialBuyData initialBuyData = new()
            {
                Guid = Guid.NewGuid().ToString(),
            };
            var ex = Assert.ThrowsAsync<Exception>(() => _coinloreService.CalculateChangeFromInitialBuyAsync(initialBuyData, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo($"Guid: {initialBuyData.Guid}, LogMessage: No data received from user"));
        }

        [Test]
        public void CalculateChangeFromInitialBuyAsyncThrowWhenZeroTickers()
        {
            InitialBuyData initialBuyData = new()
            {
                Guid = Guid.NewGuid().ToString(),
            };
            initialBuyData.InitialBuyDataFromRequestList.Add(new InitialBuyDataFromRequest() { InitialBuyPrice = 10, CryptoCurrencySymbol = "ETH", NumberOfCoins = 123.14M });
            _coinloreClientMock.GetCoinsInfoAsync([], CancellationToken.None).ReturnsForAnyArgs(new TickersData() { Data = [] });

            var ex = Assert.ThrowsAsync<Exception>(() => _coinloreService.CalculateChangeFromInitialBuyAsync(initialBuyData, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo(($"Guid: {initialBuyData.Guid}, LogMessage: No data received from Coinlore API")));
        }

        [Test]
        public async Task CalculateChangeFromInitialBuyAsyncSucceeded()
        {
            InitialBuyData initialBuyData = new()
            {
                Guid = Guid.NewGuid().ToString(),
            };
            initialBuyData.InitialBuyDataFromRequestList.Add(new InitialBuyDataFromRequest() { InitialBuyPrice = 12.12454M, CryptoCurrencySymbol = "BTC", NumberOfCoins = 24012.43M });
            initialBuyData.InitialBuyDataFromRequestList.Add(new InitialBuyDataFromRequest() { InitialBuyPrice = 10, CryptoCurrencySymbol = "ETH", NumberOfCoins = 123.14M });

            var tickersData = new TickersData()
            {
                Data =
                [
                    new Ticker(){
                        Id = "90",
                        Symbol = "BTC",
                        Name = "Bitcoin",
                        Nameid = "bitcoin",
                        Rank = 1,
                        Price_usd = "59119.49",
                        Percent_change_24h = "-2.53",
                        Percent_change_1h = "0.44",
                        Percent_change_7d = "3.66",
                        Price_btc = "1.00",
                        Market_cap_usd = "1166807656639.80",
                        Volume24 = 29458301782.254963,
                        Volume24a = 31011564057.907154,
                        Csupply = "19736430.00",
                        Tsupply = "19736430",
                        Msupply = "21000000"
                    }
                ],
                TickerInfo = new TickerInfo()
                {
                    CoinsNum = 2,
                    Time = 1635730400
                }
            }; 
            
            var tickersDataSecondRequest = new TickersData()
            {
                Data =
                [
                    new Ticker(){
                        Id = "77733",
                        Symbol = "ETH",
                        Name = "Ethereum",
                        Nameid = "ethereum",
                        Rank = 2,
                        Price_usd = "2678.01",
                        Percent_change_24h = "-1.34",
                        Percent_change_1h = "0.36",
                        Percent_change_7d = "7.97",
                        Price_btc = "0.045327",
                        Market_cap_usd = "321973091815.98",
                        Volume24 = 13166157358.048382,
                        Volume24a = 14182752588.011671,
                        Csupply = "120228315.00",
                        Tsupply = "122375302",
                        Msupply = ""
                    }
                ],
                TickerInfo = new TickerInfo()
                {
                    CoinsNum = 2,
                    Time = 1635730400
                }
            };

            var expCahnge = new List<CalculatedChangeFromInitialBuy>
            {
                new()
                {
                    CryptoCurrency = "BTC",
                    CurrentPriceInUsd = 59119.49M,
                    ChangeInPercentage = 487501.92M,
                    OverallPerCurrencyInUsd = 1419311475.5927M
                },
                new()
                {
                    CryptoCurrency = "ETH",
                    CurrentPriceInUsd = 2678.01M,
                    ChangeInPercentage = 26680.1M,
                    OverallPerCurrencyInUsd = 328538.7514M
                },
            };
            var parameterValues = new Dictionary<string, string?>
            {
                { "start", "1" },
                { "limit", "100" }
            };
            
            _coinloreClientMock.GetCoinsInfoAsync(parameterValues, Arg.Any<CancellationToken>()).ReturnsForAnyArgs(tickersDataSecondRequest);
            _coinloreClientMock.GetCoinsInfoAsync(null, Arg.Any<CancellationToken>()).Returns(tickersData);
            var result = await _coinloreService.CalculateChangeFromInitialBuyAsync(initialBuyData, CancellationToken.None);

            await _coinloreClientMock.Received(2).GetCoinsInfoAsync(Arg.Any<Dictionary<string, string?>>(), Arg.Any<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OverallChangeInPercentage, Is.EqualTo(514182.02m));
                Assert.That(result.OverallChangeInPriceUsd, Is.EqualTo(1419640014.3441m));
            });
            for (int i = 0; i < result.CalculatedChangeFromInitialBuyList.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(result.CalculatedChangeFromInitialBuyList[i].CryptoCurrency, Is.EqualTo(expCahnge[i].CryptoCurrency));
                    Assert.That(result.CalculatedChangeFromInitialBuyList[i].CurrentPriceInUsd, Is.EqualTo(expCahnge[i].CurrentPriceInUsd));
                    Assert.That(result.CalculatedChangeFromInitialBuyList[i].ChangeInPercentage, Is.EqualTo(expCahnge[i].ChangeInPercentage));
                    Assert.That(result.CalculatedChangeFromInitialBuyList[i].OverallPerCurrencyInUsd, Is.EqualTo(expCahnge[i].OverallPerCurrencyInUsd));
                });
            }
        }
    }
}
