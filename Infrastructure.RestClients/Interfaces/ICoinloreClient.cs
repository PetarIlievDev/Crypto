namespace Infrastructure.RestClients.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.RestClients.Models;

    public interface ICoinloreClient
    {
        Task<TickersData> GetCoinsInfoAsync(Dictionary<string, string?> paramValues, CancellationToken ct);
    }
}
