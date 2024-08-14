namespace WebApi.Services.Interfaces
{
    using System.Threading.Tasks;
    using WebApi.Services.Models.ChangeFromInitialBuy;

    public interface ICoinloreService
    {
        Task<CalculatedOverallChangeFromInitialBuy> CalculateChangeFromInitialBuyAsync(InitialBuyData initialBuyData, CancellationToken ct);
    }
}