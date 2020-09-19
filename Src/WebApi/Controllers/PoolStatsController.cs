using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SafeStak.Deltas.WebApi.Controllers
{
    [ApiController]
    public class PoolStatsController : ControllerBase
    {
        private readonly IPoolStatsRetriever _retriever;
        private readonly IPoolStatsSerialiser _serialiser;

        public PoolStatsController(
            IPoolStatsRetriever retriever, IPoolStatsSerialiser serialiser)
        {
            _retriever = retriever;
            _serialiser = serialiser;
        }

        [HttpGet]
        [Route("safestats/v1/pools/{poolId:required}")]
        public async Task<string> Get(string poolId, CancellationToken ct)
        {
            var stats = await _retriever.RetrievePoolStatsAsync(poolId, ct);

            return _serialiser.Serialise(stats);
        }
    }
}
