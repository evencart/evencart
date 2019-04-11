using EvenCart.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
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