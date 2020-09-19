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

        public async Task<PoolStatsResponse> RetrievePoolStatsAsync(string poolId, CancellationToken ct = default)
        {
            var summary = await _adaPoolsClient.GetPoolSummaryAsync(poolId, ct).ConfigureAwait(false);
            
            return new PoolStatsResponse
            {
                PoolId = summary.Data.Id,
                Ticker = summary.Data.Db_ticker,
                CurrentEpoch = int.Parse(summary.Data.Rewards_epoch),
                ActiveStakeLovelaces = long.Parse(summary.Data.Active_stake),
                LiveStakeLovelaces = long.Parse(summary.Data.Total_stake),
                PledgedLovelaces = long.Parse(summary.Data.Pledge),
                ActualPledgedLovelaces = long.Parse(summary.Data.Pledged),
                BlocksEpoch = int.Parse(summary.Data.Blocks_epoch),
                BlocksEpochEstimate = summary.Data.Blocks_estimated,
                BlocksLifetime = int.Parse(summary.Data.Blocks_lifetime),
                DelegatorCount = int.Parse(summary.Data.Delegators),
                SaturatedPercentage = summary.Data.Saturated,
                StatTimestamp = DateTimeOffset.Now
            };
        }
    }
}
