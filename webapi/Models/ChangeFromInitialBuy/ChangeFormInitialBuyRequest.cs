namespace WebApi.Models.ChangeFromInitialBuy
{
    public class ChangeFormInitialBuyRequest
    {
        public required string Guid { get; set; }
        public IEnumerable<ChangeFromInitialBuyDataRequest> InitialBuyDataFromRequestList { get; set; }
    }
}
