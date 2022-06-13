namespace MarketplaceBackend.Helpers
{
    public class AuthSettings
    {
        public string Issuer { get; set; }
       
        public string Audience { get; set; }

        public string Key { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}
