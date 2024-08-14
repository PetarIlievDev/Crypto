namespace WebApi.Models
{
    public class SaveToLogRequest
    {
        public required string Guid { get; set; }
        public string? LogMessage { get; set; }
    }
}
