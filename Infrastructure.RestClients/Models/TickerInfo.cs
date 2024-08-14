namespace Infrastructure.RestClients.Models
{
    using System.Text.Json.Serialization;

    public class TickerInfo
    {
        [JsonPropertyName("coins_num")]
        public int CoinsNum { get; set; }
        public int Time { get; set; }
    }
}
