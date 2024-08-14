namespace WebApi.Services
{
    using Microsoft.Extensions.Logging;
    using WebApi.Services.Interfaces;
    using WebApi.Services.Models.LogToFile;

    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        public void LogToFile(LogToFileData data)
        {
            var logMessage = $"Guid: {data.Guid}, LogMessage: {data.LogMessage}";
            _logger.LogInformation(logMessage);
        }

        public void LogToFile(string logMessage)
        {
            _logger.LogInformation(logMessage);
        }
    }
}
