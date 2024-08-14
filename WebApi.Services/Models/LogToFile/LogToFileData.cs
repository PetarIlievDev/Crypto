namespace WebApi.Services.Models.LogToFile
{
    public class LogToFileData
    {
        public required string Guid { get; set; }
        public string? LogMessage { get; set; }
    }
}
