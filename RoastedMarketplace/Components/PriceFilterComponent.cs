using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Models.Components;
using RoastedMarketplace.Models.Products;

namespace RoastedMarketplace.Components
{
    [ViewComponent(Name = "PriceFilter")]
    public class PriceFilterComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            var searchModel = (ProductSearchModel) data;

            var startPrice = searchModel?.AvailableFromPrice ?? 0;
            var endPrice = searchModel?.AvailableToPrice ?? 0;

            var model = new PriceFilterModel()
            {
                AvailableFromPrice = startPrice.FloorTen(),
                AvailableToPrice = endPrice.CeilTen(),
                FromPrice = searchModel?.FromPrice ?? startPrice,
                ToPrice = searchModel?.ToPrice ?? endPrice
            };

            return R.Success.With("prices", model).ComponentResult;
        }
    }
}