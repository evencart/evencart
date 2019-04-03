using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    public abstract class FoundationComponent : ViewComponent
    {
        public abstract IViewComponentResult Invoke(object data = null);

        public CustomResponse R => CustomResponse.ComponentResponse(this);

        public User CurrentUser => ApplicationEngine.CurrentUser;
    }
}