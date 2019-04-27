using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class ConsentGroupImplementation : FoundationModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ConsentImplementation> Consents { get; set; }

    }
}