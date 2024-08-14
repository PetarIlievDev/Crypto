namespace WebApi.Tests.MapperProfiles
{
    using System.Linq;
    using AutoMapper;
    using WebApi.Models;
    using WebApi.Models.ChangeFromInitialBuy;
    using WebApi.Services.Models.ChangeFromInitialBuy;
    using WebApi.Services.Models.LogToFile;

    public class MappingProfileTests
    {
        IMapper _mapper;
        MapperConfiguration _config;
        private static readonly string[] assemblyNamesToScan = ["WebApi"];

        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg => cfg.AddMaps(assemblyNamesToScan));
            _mapper = _config.CreateMapper();
        }

        [Test]
        public void ChangeFormInitialBuyRequestMappingTest()
        {
            ChangeFormInitialBuyRequest source = new()
            {
                Guid = "347c0fce-b4a7-40b0-b67a-705d07498358",
                InitialBuyDataFromRequestList =
                [
                    new()
                    { 
                        InitialBuyPrice = 24012.43M,
                        CryptoCurrencySymbol = "BTC",
                        NumberOfCoins = 12.12454M
                    },
                    new()
                    {
                        InitialBuyPrice = 123.24M,
                        CryptoCurrencySymbol = "ETH",
                        NumberOfCoins = 10
                    }
                ]
            };
            var sourceAsList = source.InitialBuyDataFromRequestList.ToList();
            var destination = _mapper.Map<InitialBuyData>(source);

            Assert.That(destination, Is.Not.Null);
            Assert.That(destination.Guid, Is.EqualTo(source.Guid));
            for (int i = 0; i < destination.InitialBuyDataFromRequestList.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(destination.InitialBuyDataFromRequestList[i].InitialBuyPrice, Is.EqualTo(sourceAsList[i].InitialBuyPrice));
                    Assert.That(destination.InitialBuyDataFromRequestList[i].CryptoCurrencySymbol, Is.EqualTo(sourceAsList[i].CryptoCurrencySymbol));
                    Assert.That(destination.InitialBuyDataFromRequestList[i].NumberOfCoins, Is.EqualTo(sourceAsList[i].NumberOfCoins));
                });
            }
        }

        [Test]
        public void CalculatedOverallChangeFromInitialBuyMappingTest()
        {
            CalculatedOverallChangeFromInitialBuy source = new()
            {
                CalculatedChangeFromInitialBuyList =
                [
                    new()
                    {
                        CryptoCurrency = "BTC",
                        CurrentPriceInUsd = 24012.43M,
                        ChangeInPercentage = 0.12M,
                        OverallPerCurrencyInUsd = 12.12454M
                    },
                    new()
                    {
                        CryptoCurrency = "ETH",
                        CurrentPriceInUsd = 123.24M,
                        ChangeInPercentage = 0.12M,
                        OverallPerCurrencyInUsd = 10
                    }
                ],
                OverallChangeInPercentage = 0.12M,
                OverallChangeInPriceUsd = 123.32M
            };
            var sourceAsList = source.CalculatedChangeFromInitialBuyList.ToList();
            var destination = _mapper.Map<OverallChangeFromInitalBuyResponse>(source);

            Assert.That(destination, Is.Not.Null);
            for (int i = 0; i < destination.CalculatedChangeFromInitialBuyList.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(destination.CalculatedChangeFromInitialBuyList[i].CryptoCurrency, Is.EqualTo(sourceAsList[i].CryptoCurrency));
                    Assert.That(destination.CalculatedChangeFromInitialBuyList[i].CurrentPriceInUsd, Is.EqualTo(sourceAsList[i].CurrentPriceInUsd));
                    Assert.That(destination.CalculatedChangeFromInitialBuyList[i].ChangeInPercentage, Is.EqualTo(sourceAsList[i].ChangeInPercentage));
                    Assert.That(destination.CalculatedChangeFromInitialBuyList[i].OverallPerCurrencyInUsd, Is.EqualTo(sourceAsList[i].OverallPerCurrencyInUsd));
                });
            }
        }

        [Test]
        public void SaveToLogRequestMappingTest()
        {
            SaveToLogRequest source = new()
            {
                Guid = "347c0fce-b4a7-40b0-b67a-705d07498358",
                LogMessage = "User requests is for 2 crypto coins."
            };
            var destination = _mapper.Map<LogToFileData>(source);

            Assert.Multiple(() =>
            {
                Assert.That(destination, Is.Not.Null);
                Assert.That(destination.Guid, Is.EqualTo(source.Guid));
                Assert.That(destination.LogMessage, Is.EqualTo(source.LogMessage));
            });
        }
    }
}
