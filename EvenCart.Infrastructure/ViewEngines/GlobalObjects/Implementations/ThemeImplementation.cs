using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class ThemeImplementation : FoundationModel
    {
        public string Url { get; set; }

        public string Name { get; set; }
    }
}