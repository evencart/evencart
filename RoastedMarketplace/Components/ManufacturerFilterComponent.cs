using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Components
{
    [ViewComponent(Name = "ManufacturerFilter")]
    public class ManufacturerFilterComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            throw new System.NotImplementedException();
        }
    }
}