using System.Collections.Generic;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Models.Components;
using EvenCart.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "PriceFilter")]
    public class PriceFilterComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var searchModel = dataAsDict["model"] as ProductSearchModel;
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