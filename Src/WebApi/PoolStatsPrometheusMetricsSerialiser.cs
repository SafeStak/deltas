using System.Text;

namespace SafeStak.Deltas.WebApi
{
    public interface IPoolStatsSerialiser
    {
        string Serialise(PoolStatsResponse stats);
    }

    public class PoolStatsPrometheusMetricsSerialiser : IPoolStatsSerialiser
    {
        public string Serialise(PoolStatsResponse stats)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"safestats_timestamp_updated {stats.StatTimestamp.Ticks}");
            sb.AppendLine($"safestats_pool_id {stats.PoolId}");
            sb.AppendLine($"safestats_pool_ticker {stats.Ticker}");
            sb.AppendLine($"safestats_pool_active_stake {stats.ActiveStakeLovelaces}");
            sb.AppendLine($"safestats_pool_live_stake {stats.LiveStakeLovelaces}");
            sb.AppendLine($"safestats_pool_actual_pledge {stats.ActualPledgedLovelaces}");
            sb.AppendLine($"safestats_pool_blocks_epoch {stats.BlocksEpoch}");
            sb.AppendLine($"safestats_pool_blocks_epoch_estimate {stats.BlocksEpochEstimate}");
            sb.AppendLine($"safestats_pool_lifetime {stats.BlocksLifetime}");
            sb.AppendLine($"safestats_pool_delegator_count {stats.DelegatorCount}");
            sb.AppendLine($"safestats_pool_saturated_percentage {stats.SaturatedPercentage}");

            return sb.ToString();
        }
    }
}
