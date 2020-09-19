using System.Threading;
using System.Threading.Tasks;

namespace SafeStak.Deltas.WebApi
{
    public interface IPoolStatsRetriever
    {
        Task<PoolStats> RetrievePoolStatsAsync(string poolId, CancellationToken ct = default);
    }
}
