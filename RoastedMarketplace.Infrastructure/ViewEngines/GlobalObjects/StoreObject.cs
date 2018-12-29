using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class StoreObject : GlobalObject
    {
        public override object GetObject()
        {
            return new StoreImplementation()
            {
                Url = "",
                Name = "Store Name",
                Theme = new ThemeImplementation()
                {
                    Name = "Default",
                    Url = "/default"
                }
            };
        }
    }
}