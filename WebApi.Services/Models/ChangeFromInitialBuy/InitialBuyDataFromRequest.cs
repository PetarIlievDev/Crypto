namespace WebApi.Services.Models.ChangeFromInitialBuy
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class InitialBuyDataFromRequest
    {
        public decimal NumberOfCoins { get; set; }
        public required string CryptoCurrencySymbol { get; set; }
        public decimal InitialBuyPrice { get; set; }
    }
}
