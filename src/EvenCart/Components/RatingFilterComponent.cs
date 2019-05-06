using EvenCart.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "RatingFilter")]
    public class RatingFilterComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            throw new System.NotImplementedException();
        }
    }
}