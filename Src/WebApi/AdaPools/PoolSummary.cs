namespace SafeStak.Deltas.WebApi.AdaPools
{
    public class PoolData
    {
        public string Id { get; set; }
        public string Pool_id { get; set; }
        public string Db_ticker { get; set; }
        public string Db_name { get; set; }
        public string Db_url { get; set; }
        public string Db_verified { get; set; }
        public string Total_stake { get; set; }
        public string Rewards_epoch { get; set; }
        public string Tax_ratio { get; set; }
        public string Tax_fix { get; set; }
        public string Roa { get; set; }
        public string Blocks_epoch { get; set; }
        public string Blocks_lifetime { get; set; }
        public string Pledge { get; set; }
        public string Delegators { get; set; }
        public string Pledged { get; set; }
        public double Tax_real { get; set; }
        public string Active_stake { get; set; }
        public string Active_blocks { get; set; }
        public double Saturated { get; set; }
        public int Rank { get; set; }
        public double Blocks_estimated { get; set; }
        public string Db_description { get; set; }
        public string Owners { get; set; }
    }

    public class PoolSummary
    {
        public string Created { get; set; }
        public PoolData Data { get; set; }
        public string Updated { get; set; }

    }
}
