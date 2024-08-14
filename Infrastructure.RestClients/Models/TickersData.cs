namespace Infrastructure.RestClients.Models
{
    using System.Collections.Generic;

    public class TickersData
    {
        public List<Ticker> Data { get; set; }

        public TickerInfo TickerInfo { get; set; }
    }
}
