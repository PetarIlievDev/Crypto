namespace Infrastructure.RestClients.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.RestClients.Models;

    public interface ICoinloreClient
    {
        Task<List<Ticker>> GetCoinsInfoAsync(Dictionary<string, string?> paramValues, CancellationToken ct);
    }
}
