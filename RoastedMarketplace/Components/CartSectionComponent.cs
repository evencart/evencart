using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Components
{
    [ViewComponent(Name = "CartSection")]
    public class CartSectionComponent : FoundationComponent
    {
        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDictionary = (Dictionary<string, object>) data ?? new Dictionary<string, object>()
            {
                {"checkout", "false" }
            };
            dataAsDictionary.TryGetValue("checkout", out object checkoutValue);
            checkoutValue = checkoutValue ?? "false";
            return R.With("checkout", checkoutValue).ComponentResult;
        }
    }
}