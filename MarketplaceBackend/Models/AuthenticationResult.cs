namespace MarketplaceBackend.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public User User { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
