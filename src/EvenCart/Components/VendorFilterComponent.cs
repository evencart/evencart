using EvenCart.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "VendorFilter")]
    public class VendorFilterComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            throw new System.NotImplementedException();
        }
    }
}