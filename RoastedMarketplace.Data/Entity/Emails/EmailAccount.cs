using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Emails
{
    public class EmailAccount : FoundationEntity
    {
        public string Email { get; set; }

        public string FromName { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool UseSsl { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsDefault { get; set; }
    }
}