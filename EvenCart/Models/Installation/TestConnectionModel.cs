using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Installation
{
    public class TestConnectionModel : FoundationModel
    {
        public string ProviderName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string ServerUrl { get; set; }

        public string DatabaseUserName { get; set; }

        public string DatabasePassword { get; set; }

        public bool IsConnectionString { get; set; }

        public bool IntegratedSecurity { get; set; }
    }
}