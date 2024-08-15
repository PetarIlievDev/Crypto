namespace WebApi.Services.Tests
{
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using WebApi.Services.Models.LogToFile;

    public class LogServiceTests
    {
        private readonly ILogger<LogService> _loggerMock;
        private readonly LogService _logService;

        public LogServiceTests()
        {
            _loggerMock = Substitute.For<ILogger<LogService>>();
            _logService = new LogService(_loggerMock);            
        }

        [Test]
        public void LogToFileSuccessfull()
        {
            var logtoFileData = new LogToFileData
            {
                Guid = Guid.NewGuid().ToString(),
                LogMessage = "Test log message"
            };

            _logService.LogToFile(logtoFileData);

            _loggerMock.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Equals($"Guid: {logtoFileData.Guid}, LogMessage: {logtoFileData.LogMessage}")),
                null,
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Test]
        public void LogToFileWithStringSuccessfull()
        {
            var logMessage = $"Guid: {Guid.NewGuid()}, LogMessage: No data received from user";

            _logService.LogToFile(logMessage);

            _loggerMock.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Equals(logMessage)),
                null,
                Arg.Any<Func<object, Exception?, string>>());
        }
    }
}