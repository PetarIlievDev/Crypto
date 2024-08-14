namespace WebApi.Tests.Controllers
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using Webapi.Controllers;
    using WebApi.Services.Interfaces;

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
        public void Test1()
        {
            Assert.Pass();
        }
    }
}