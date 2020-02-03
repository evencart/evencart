using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class NavigationImplementation : FoundationModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Css { get; set; }

        public bool IsGroup { get; set; }

        public bool OpenInNewWindow { get; set; }

        public IList<NavigationImplementation> Children { get; set; }

        public int Id { get; set; }
    }
}