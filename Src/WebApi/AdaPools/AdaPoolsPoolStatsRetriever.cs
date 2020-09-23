using System;
using System.Threading;
using System.Threading.Tasks;

namespace SafeStak.Deltas.WebApi.AdaPools
{
    public class AdaPoolsPoolStatsRetriever : IPoolStatsRetriever
    {
        private readonly IAdaPoolsApiClient _adaPoolsClient;

        public AdaPoolsPoolStatsRetriever(IAdaPoolsApiClient adaPoolsClient)
        {
            _adaPoolsClient = adaPoolsClient;
        }

        public async Task<PoolStats> RetrievePoolStatsAsync(string poolId, CancellationToken ct = default)
        {
            var summary = await _adaPoolsClient.GetPoolSummaryAsync(poolId, ct).ConfigureAwait(false);
            
            return new PoolStats
            {
                PoolId = summary.Data.Id,
                Ticker = summary.Data.Db_ticker,
                RewardsEpoch = int.Parse(summary.Data.Rewards_epoch),
                ActiveStakeLovelaces = long.Parse(summary.Data.Active_stake),
                LiveStakeLovelaces = long.Parse(summary.Data.Total_stake),
                PledgedLovelaces = long.Parse(summary.Data.Pledge),
                ActualPledgedLovelaces = long.Parse(summary.Data.Pledged),
                BlocksEpoch = int.Parse(summary.Data.Blocks_epoch),
                BlocksEpochEstimate = summary.Data.Blocks_estimated,
                BlocksLifetime = int.Parse(summary.Data.Blocks_lifetime) + int.Parse(summary.Data.Blocks_epoch),
                DelegatorCount = int.Parse(summary.Data.Delegators),
                Saturated = summary.Data.Saturated,
                StatTimestamp = DateTimeOffset.Now
            };
        }
    }
}
