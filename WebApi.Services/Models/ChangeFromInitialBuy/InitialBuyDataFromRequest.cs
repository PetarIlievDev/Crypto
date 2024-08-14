namespace WebApi.Services.Models.ChangeFromInitialBuy
{
    public class InitialBuyDataFromRequest
    {
        public decimal NumberOfCoins { get; set; }
        public required string CryptoCurrencySymbol { get; set; }
        public decimal InitialBuyPrice { get; set; }
    }
}
