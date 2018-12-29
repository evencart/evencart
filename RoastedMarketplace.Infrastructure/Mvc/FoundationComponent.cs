using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    public abstract class FoundationComponent : ViewComponent
    {
        public abstract IViewComponentResult Invoke(object data = null);

        public CustomResponse R => CustomResponse.ComponentResponse(this);
    }
}