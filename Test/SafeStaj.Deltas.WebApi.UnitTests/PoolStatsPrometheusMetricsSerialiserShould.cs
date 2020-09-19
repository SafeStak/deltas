using SafeStak.Deltas.WebApi;
using System;
using Xunit;

namespace SafeStaj.Deltas.WebApi.UnitTests
{
    public class PoolStatsPrometheusMetricsSerialiserShould
    {
        private readonly PoolStatsPrometheusMetricsSerialiser _serialiser;

        public PoolStatsPrometheusMetricsSerialiserShould()
        {
            _serialiser = new PoolStatsPrometheusMetricsSerialiser();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(10000, 10000, 1, 1, 10000, 10000, 10, 1, 0.001)]
        public void Serialise_Fields_Correctly(
            long activeStakeLovelaces,
            long liveStakeLovelaces,
            int blocksEpoch,
            double blocksEpochEstimate,
            long pledgedLovelaces,
            long actualPledgedLovelaces,
            int blocksLifetime,
            int delegatorCount,
            double saturated)
        {
            var stats = new PoolStats
            {
                ActiveStakeLovelaces = activeStakeLovelaces,
                LiveStakeLovelaces = liveStakeLovelaces,
                BlocksEpoch = blocksEpoch,
                BlocksEpochEstimate = blocksEpochEstimate,
                PledgedLovelaces = pledgedLovelaces,
                ActualPledgedLovelaces = actualPledgedLovelaces,
                BlocksLifetime = blocksLifetime,
                DelegatorCount = delegatorCount,
                Saturated = saturated
            };

            var serialised = _serialiser.Serialise(stats);

            var lines = serialised.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            Assert.Contains($"safestats_pool_active_stake {activeStakeLovelaces}", lines);
            Assert.Contains($"safestats_pool_live_stake {liveStakeLovelaces}", lines);
            Assert.Contains($"safestats_pool_lifetime {blocksLifetime}", lines);
            Assert.Contains($"safestats_pool_blocks_epoch {blocksEpoch}", lines);
            Assert.Contains($"safestats_pool_blocks_epoch_estimate {blocksEpochEstimate}", lines);
            Assert.Contains($"safestats_pool_delegator_count {delegatorCount}", lines);
            Assert.Contains($"safestats_pool_saturated {saturated}", lines);
            Assert.Contains($"safestats_pool_pledge {pledgedLovelaces}", lines);
            Assert.Contains($"safestats_pool_actual_pledge {actualPledgedLovelaces}", lines);
        }
    }
}
