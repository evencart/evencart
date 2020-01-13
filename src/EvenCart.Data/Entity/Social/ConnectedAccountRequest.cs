namespace EvenCart.Data.Entity.Social
{
    public class ConnectedAccountRequest
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ProviderName { get; set; }

        public string ProviderUserId { get; set; }

        public string AccessToken { get; set; }

        public bool AutoLogin { get; set; }
    }
}