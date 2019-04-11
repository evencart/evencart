using EvenCart.Data.Entity.Users;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Infrastructure.Mvc
{
    public abstract class FoundationComponent : ViewComponent
    {
        public abstract IViewComponentResult Invoke(object data = null);

        public CustomResponse R => CustomResponse.ComponentResponse(this);

        public User CurrentUser => ApplicationEngine.CurrentUser;
    }
}