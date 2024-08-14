namespace WebApi.Services.Interfaces
{
    using WebApi.Services.Models.LogToFile;

    public interface ILogService
    {
        void LogToFile(LogToFileData data);
        void LogToFile(string logMessage);
    }
}
