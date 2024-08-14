namespace WebApi.Services.Models.ChangeFromInitialBuy
{
    using System.Collections.Generic;

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
