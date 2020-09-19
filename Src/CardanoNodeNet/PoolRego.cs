namespace SafeStak.Deltas.CardanoNodeNet
{
    public class PoolRego
    {
        public class Relay
        {
            public string SingleHostName { get; set; }
            public string SingleHostAddressIpV6 { get; set; }
            public string SingleHostAddressIpV4 { get; set; }
            public int Port { get; set; }
        }

        public class RewardAccount
        {
            public string CredentialKeyHash { get; set; }
            public string Network { get; set; }
        }

        public string PoolId { get; set; }
        public string Ticker { get; set; }
        public string[] Owners { get; set; }
        public string RewardAccountCredentialHash { get; set; }
        public long Cost { get; set; }
        public double Margin { get; set; }
        public long Pledge { get; set; }
        public string Vrf { get; set; }
        public string MetadataHash { get; set; }
        public string MetadataUrl { get; set; }
        public Relay[] Relays { get; set; }
        public RewardAccount RewardAcct { get; set; }
    }
}
