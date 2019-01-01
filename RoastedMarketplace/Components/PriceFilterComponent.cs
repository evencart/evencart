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
            if (searchModel == null)
                return R.Success.ComponentResult;
            var startPrice = searchModel.AvailableFromPrice;
            var endPrice = searchModel.AvailableToPrice;

            var model = new PriceFilterModel()
            {
                AvailableFromPrice = startPrice.FloorTen(),
                AvailableToPrice = endPrice.CeilTen(),
            };
            model.FromPrice = searchModel.FromPrice ?? model.AvailableFromPrice;
            model.ToPrice = searchModel.ToPrice ?? model.AvailableToPrice;
            return R.Success.With("prices", model).ComponentResult;
        }
    }
}