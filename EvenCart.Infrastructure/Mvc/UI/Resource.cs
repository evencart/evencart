using System.Collections.Generic;

namespace RoastedMarketplace.Infrastructure.Mvc.UI
{
    public class Resource
    {
       
        public string ResourceName { get; set; }

        public string ResourcePath { get; set; }

        public ResourcePlacementType PlacementType { get; set; }

        public ResourceRegistrationType RegistrationType { get; set; }

        public bool Enqueued { get; set; }

        public bool Rendered { get; set; }

        public IList<Resource> DependsOn { get; set; }
    }
}