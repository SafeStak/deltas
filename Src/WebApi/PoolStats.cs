using System;

namespace SafeStak.Deltas.WebApi
{
    public class PoolStats
    {
        public DateTimeOffset StatTimestamp { get; set; }
        public long ActiveStakeLovelaces { get; set; }
        public long LiveStakeLovelaces { get; set; }
        public int BlocksEpoch { get; set; }
        public double BlocksEpochEstimate { get; set; }
        public long PledgedLovelaces { get; set; }
        public long ActualPledgedLovelaces { get; set; }
        public int BlocksLifetime { get; set; }
        public int DelegatorCount { get; set; }
        public double Saturated { get; set; }
        public string PoolId { get; set; }
        public string Ticker { get; set; }
        public int RewardsEpoch { get; set; }
    }
}
