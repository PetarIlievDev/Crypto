namespace Infrastructure.RestClients.Models
{
    public class Ticker
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Nameid { get; set; }
        public int Rank { get; set; }
        public string Price_usd { get; set; }
        public string Percent_change_24h { get; set; }
        public string Percent_change_1h { get; set; }
        public string Percent_change_7d { get; set; }
        public string Price_btc { get; set; }
        public string Market_cap_usd { get; set; }
        public double Volume24 { get; set; }
        public double Volume24a { get; set; }
        public string Csupply { get; set; }
        public string Tsupply { get; set; }
        public string Msupply { get; set; }

    }
}
