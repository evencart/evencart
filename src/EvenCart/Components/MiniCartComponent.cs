using EvenCart.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
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