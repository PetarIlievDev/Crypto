namespace WebApi.Tests.Controllers
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Webapi.Controllers;
    using WebApi.Services.Interfaces;
    using WebApi.Services.Models.ChangeFromInitialBuy;

    public class CoinsPortfolioControllerTests
    {
        private ILogger<CoinsPortfolioController> _loggerMock;
        private IMapper _mapperMock;
        private ICoinloreService _coinloreServiceMock;
        private CoinsPortfolioController _coinsPortfolioController;

        [SetUp]
        public void Setup()
        {
            _loggerMock = Substitute.For<ILogger<CoinsPortfolioController>>();
            _mapperMock = Substitute.For<IMapper>();
            _coinloreServiceMock = Substitute.For<ICoinloreService>();
            _coinsPortfolioController = new CoinsPortfolioController(_loggerMock, _mapperMock, _coinloreServiceMock);
        }

        [Test]
        public async Task CalculatePercentageFromInitalBuyAsyncCallService()
        {
            await _coinsPortfolioController.CalculatePercentageFromInitalBuyAsync(null, CancellationToken.None);
            _coinloreServiceMock.CalculateChangeFromInitialBuyAsync(Arg.Any<InitialBuyData>(), Arg.Any<CancellationToken>()).ReturnsNull();
            await _coinloreServiceMock.Received(1).CalculateChangeFromInitialBuyAsync(Arg.Any<InitialBuyData>(), CancellationToken.None);
        }
    }
}