namespace SafeStak.Deltas.WebApi.Domain
{
    public class Pool
    {
        public string PoolId { get; set; }
        public string Ticker { get; set; }
        public string Owner { get; }
        public string RewardAccountCredentialHash { get; }
        public long Cost { get; }
        public double Margin { get; }
        public long Pledge { get; }
        public string Vrf { get; }
        public string MetadataHash { get; set; }
        public string MetadataUrl { get; set; }
    }
}
