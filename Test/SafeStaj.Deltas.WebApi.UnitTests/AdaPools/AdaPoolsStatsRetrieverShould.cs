using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Moq;
using SafeStak.Deltas.WebApi.AdaPools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SafeStaj.Deltas.WebApi.UnitTests.AdaPools
{
    public class AdaPoolsStatsRetrieverShould
    {
        private readonly Mock<IAdaPoolsApiClient> _mockClient;
        private readonly AdaPoolsPoolStatsRetriever _retriever;

        public AdaPoolsStatsRetrieverShould()
        {
            _mockClient = new Mock<IAdaPoolsApiClient>();
            _retriever = new AdaPoolsPoolStatsRetriever(_mockClient.Object);
        }

        [Theory]
        [InlineData("00000000000000000000000000000000000000000000000000000000", "0", "0", "0", 0, "0", "Description", "Name", "Ticker", "https://www.pool.com", "0", "0", "0", "0", "0", "0", 0)]
        [InlineData("74a10b8241fc67a17e189a58421506b7edd629ac490234933afbed97", "11", "2200001", "2", 1.1, "10", "Description", "{SAFE}STAK", "SAFE", "https://www.safestak.com", "20", "550000000000", "555000000000", "5.5", "5500000000000", "200", 0.5)]
        public async Task Map_Fields_From_Ada_Pools_Summary_Correctly(
            string id,
            string activeBlocks,
            string activeStake,
            string blocksEpoch,
            double blocksEstimated,
            string blocksLifetime,
            string description,
            string name,
            string ticker,
            string url,
            string delegators,
            string pledge,
            string pledged,
            string roa,
            string totalStake,
            string rewardsEpoch,
            double saturated)
        {
            var poolId = "74a10b8241fc67a17e189a58421506b7edd629ac490234933afbed97";
            _mockClient
                .Setup(c => c.GetPoolSummaryAsync(poolId, CancellationToken.None))
                .ReturnsAsync(new PoolSummary
                {
                    Created = "Wed, 1 Jan 2020 00:00:00 +0000",
                    Updated = "Wed, 1 Jan 2020 11:11:11 +0000",
                    Data = new PoolData
                    {
                        Active_blocks = activeBlocks,
                        Active_stake = activeStake,
                        Blocks_epoch = blocksEpoch,
                        Blocks_estimated = blocksEstimated,
                        Blocks_lifetime = blocksLifetime,
                        Db_description = description,
                        Db_name = name,
                        Db_ticker = ticker,
                        Db_url = url,
                        Delegators = delegators,
                        Pledge = pledge,
                        Pledged = pledged,
                        Pool_id = id,
                        Roa = roa,
                        Total_stake = totalStake,
                        Rewards_epoch = rewardsEpoch,
                        Saturated = saturated
                    }
                });

            var poolStats = await _retriever.RetrievePoolStatsAsync(poolId);

            poolStats.PoolId.Should().Be(id);
            poolStats.Ticker.Should().Be(ticker);
            poolStats.RewardsEpoch.Should().Be(int.Parse(rewardsEpoch));
            poolStats.ActiveStakeLovelaces.Should().Be(long.Parse(activeStake));
            poolStats.BlocksLifetime.Should().Be(int.Parse(blocksLifetime) + int.Parse(blocksEpoch));
            poolStats.BlocksEpoch.Should().Be(int.Parse(blocksEpoch));
            poolStats.ActualPledgedLovelaces.Should().Be(long.Parse(pledged));
            poolStats.BlocksEpochEstimate.Should().Be(blocksEstimated);
            poolStats.DelegatorCount.Should().Be(int.Parse(delegators));
            poolStats.Saturated.Should().Be(saturated);
            poolStats.PledgedLovelaces.Should().Be(long.Parse(pledge));
            poolStats.ActiveStakeLovelaces.Should().Be(long.Parse(activeStake));
            poolStats.LiveStakeLovelaces.Should().Be(long.Parse(totalStake));
        }
    }
}
