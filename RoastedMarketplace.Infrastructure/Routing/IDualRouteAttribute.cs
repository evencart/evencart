namespace RoastedMarketplace.Infrastructure.Routing
{
    public interface IDualRouteAttribute
    {
        bool OnlyApi { get; set; }
    }
}