namespace WebApi.Tests.Controllers
{
    using AutoMapper;
    using NSubstitute;
    using WebApi.Controllers;
    using WebApi.Models;
    using WebApi.Services.Interfaces;
    using WebApi.Services.Models.LogToFile;

    internal class LogControllerTests
    {
        private IMapper _mapperMock;
        private ILogService _logServiceMock;
        private LogController _logController;

        [SetUp]
        public void Setup()
        {
            _mapperMock = Substitute.For<IMapper>();
            _logServiceMock = Substitute.For<ILogService>();
            _logController = new LogController(_logServiceMock, _mapperMock);
        }

        [TearDown]
        public void TearDown()
        {
            _logController.Dispose();
        }


        [Test]
        public void CalculatePercentageFromInitalBuyAsyncCallService()
        {
            _logServiceMock.LogToFile(Arg.Any<LogToFileData>());
            _logController.WriteToLog(Arg.Any<SaveToLogRequest>());
            _logServiceMock.Received(1).LogToFile(Arg.Any<LogToFileData>());
        }
    }
}
