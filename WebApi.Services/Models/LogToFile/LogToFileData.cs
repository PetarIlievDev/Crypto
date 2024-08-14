namespace WebApi.Services.Models.LogToFile
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class LogToFileData
    {
        public required string Guid { get; set; }
        public string? LogMessage { get; set; }
    }
}
