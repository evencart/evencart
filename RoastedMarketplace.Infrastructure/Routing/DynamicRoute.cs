namespace RoastedMarketplace.Infrastructure.Routing
{
    public class DynamicRoute
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public int Id { get; set; }

        public string IdTypeName { get; set; }
    }
}