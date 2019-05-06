namespace EvenCart.Data.Database
{
    public class ConnectionStringRequest
    {
        public string ProviderName { get; set; }

        public string ServerName { get; set; }

        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IntegratedSecurity { get; set; }

        public int Timeout { get; set; }
    }
}