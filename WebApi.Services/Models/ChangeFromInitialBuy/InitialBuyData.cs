namespace WebApi.Services.Models.ChangeFromInitialBuy
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class InitialBuyData
    {
        public InitialBuyData()
        {
            InitialBuyDataFromRequestList = [];
        }
        public List<InitialBuyDataFromRequest> InitialBuyDataFromRequestList { get; set; }

        public required string Guid { get; set; }
    }
}
