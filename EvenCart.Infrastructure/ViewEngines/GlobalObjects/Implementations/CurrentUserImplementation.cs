using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
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

        public bool IsImitator { get; set; }

        public string ImageUrl { get; set; }

        public IList<string> Capabilities { get; set; }
    }
}