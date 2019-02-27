namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class BreadcrumbObject : GlobalObject
    {
        public override object GetObject()
        {
            return ApplicationEngine.CurrentHttpContext.GetBreadcrumb();
        }
    }
}