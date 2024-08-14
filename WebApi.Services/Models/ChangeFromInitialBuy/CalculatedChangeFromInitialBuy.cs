namespace WebApi.Services.Models.ChangeFromInitialBuy
{
    public class CalculatedChangeFromInitialBuy
    {
        public required string CryptoCurrency { get; set; }
        public decimal CurrentPriceInUsd { get; set; }
        public decimal ChangeInPercentage { get; set; }
        public decimal OverallPerCurrencyInUsd { get; set; }
    }
}
