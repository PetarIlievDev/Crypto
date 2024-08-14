namespace WebApi.Models.ChangeFromInitialBuy
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ChangeFromInitialBuyDataResponse
    {
        public required string CryptoCurrency { get; set; }
        public decimal CurrentPriceInUsd { get; set; }
        public decimal ChangeInPercentage { get; set; }
        public decimal OverallPerCurrencyInUsd { get; set; }
    }
}
