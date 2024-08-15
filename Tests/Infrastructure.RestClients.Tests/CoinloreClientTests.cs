namespace Infrastructure.RestClients.Tests
{
    using System.Net;
    using Infrastructure.RestClients.Models;
    using Infrastructure.RestClients.Tests.Helpers;

    public class CoinloreClientTests
    {
        private CoinloreClient? _coinloreClient;

        [Test]
        public async Task GetCoinsInfoAsyncReturnCorrectData()
        {
            var expCahnge = new TickersData()
            {
                Data =
                [
                    new Ticker()
                    {
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
                    },
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
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, expCahnge);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://crypto-test.com")
            };

            _coinloreClient = new CoinloreClient(client);
            var parameterValues = new Dictionary<string, string?>
                    {
                        { "start", "2" },
                        { "limit", "100" }
                    };

            var result = await _coinloreClient.GetCoinsInfoAsync(parameterValues, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.TickerInfo.CoinsNum, Is.EqualTo(expCahnge.TickerInfo.CoinsNum));
                Assert.That(result.TickerInfo.Time, Is.EqualTo(expCahnge.TickerInfo.Time));
            });
            for (int i = 0; i < result.Data.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(result.Data[i].Id, Is.EqualTo(expCahnge.Data[i].Id));
                    Assert.That(result.Data[i].Symbol, Is.EqualTo(expCahnge.Data[i].Symbol));
                    Assert.That(result.Data[i].Name, Is.EqualTo(expCahnge.Data[i].Name));
                    Assert.That(result.Data[i].Nameid, Is.EqualTo(expCahnge.Data[i].Nameid));
                    Assert.That(result.Data[i].Rank, Is.EqualTo(expCahnge.Data[i].Rank));
                    Assert.That(result.Data[i].Price_usd, Is.EqualTo(expCahnge.Data[i].Price_usd));
                    Assert.That(result.Data[i].Percent_change_1h, Is.EqualTo(expCahnge.Data[i].Percent_change_1h));
                    Assert.That(result.Data[i].Percent_change_24h, Is.EqualTo(expCahnge.Data[i].Percent_change_24h));
                    Assert.That(result.Data[i].Percent_change_7d, Is.EqualTo(expCahnge.Data[i].Percent_change_7d));
                    Assert.That(result.Data[i].Price_btc, Is.EqualTo(expCahnge.Data[i].Price_btc));
                    Assert.That(result.Data[i].Market_cap_usd, Is.EqualTo(expCahnge.Data[i].Market_cap_usd));
                    Assert.That(result.Data[i].Volume24, Is.EqualTo(expCahnge.Data[i].Volume24));
                    Assert.That(result.Data[i].Volume24a, Is.EqualTo(expCahnge.Data[i].Volume24a));
                    Assert.That(result.Data[i].Csupply, Is.EqualTo(expCahnge.Data[i].Csupply));
                    Assert.That(result.Data[i].Tsupply, Is.EqualTo(expCahnge.Data[i].Tsupply));
                    Assert.That(result.Data[i].Msupply, Is.EqualTo(expCahnge.Data[i].Msupply));
                });
            }
        }

        [Test]
        public async Task GetCoinsInfoAsyncReturnEmptyTickerOnNullResponse()
        {
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, null);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://crypto-test.com")
            };

            _coinloreClient = new CoinloreClient(client);

            var result = await _coinloreClient.GetCoinsInfoAsync(null, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetCoinsInfoAsyncThrowException()
        {
            var handler = new MockExceptionHttpMessageHandler(HttpStatusCode.OK, null);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://crypto-test.com")
            };

            _coinloreClient = new CoinloreClient(client);

            var ex = Assert.ThrowsAsync<Exception>(() => _coinloreClient.GetCoinsInfoAsync(null, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("REST Service connect error: Exception thrown for testing purposes"));
        }
    }
}