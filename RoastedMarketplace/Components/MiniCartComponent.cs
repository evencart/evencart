using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Components
{
    [ViewComponent(Name = "MiniCart")]
    public class MiniCartComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            return R.Success.ComponentResult;
        }
    }
}