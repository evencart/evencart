using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class CurrentUserImplementation : FoundationModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsVisitor { get; set; }
        
        public bool IsVendor { get; set; }

        public string ImageUrl { get; set; }
    }
}