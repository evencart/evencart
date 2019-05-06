using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class ConsentImplementation : FoundationModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public int Id { get; set; }
    }
}