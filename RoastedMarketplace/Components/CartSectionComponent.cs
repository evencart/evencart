using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Components
{
    [ViewComponent(Name = "CartSection")]
    public class CartSectionComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            return R.ComponentResult;
        }
    }
}