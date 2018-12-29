using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Components
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