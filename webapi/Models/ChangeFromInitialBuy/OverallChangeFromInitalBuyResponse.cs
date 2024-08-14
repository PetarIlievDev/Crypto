namespace WebApi.Models.ChangeFromInitialBuy
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class OverallChangeFromInitalBuyResponse
    {
        public required List<ChangeFromInitialBuyDataResponse> CalculatedChangeFromInitialBuyList { get; set; }

        public decimal OverallChangeInPercentage { get; set; }
        public decimal OverallChangeInPriceUsd { get; set; }
    }
}
