using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class AvailableAttributesController : FoundationAdminController
    {
        private readonly IAvailableAttributeService _availableAttributeService;
        private readonly IAvailableAttributeValueService _availableAttributeValueService;
        public AvailableAttributesController(IAvailableAttributeService availableAttributeService, IAvailableAttributeValueService availableAttributeValueService)
        {
            _availableAttributeService = availableAttributeService;
            _availableAttributeValueService = availableAttributeValueService;
        }


        [DualGet("suggestions", Name = AdminRouteNames.GetAttributeSuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        public IActionResult AttributeSuggestions(string q = null)
        {
            var attributes = _availableAttributeService.Get(x => true);
            var model = new List<AutocompleteModel>();
            foreach (var c in attributes)
            {
                model.Add(new AutocompleteModel() {
                    Id = c.Id,
                    Text = c.Name
                });
            }
            return R.Success.With("suggestions", model.OrderBy(x => x.Text)).Result;
        }

        [DualGet("values/suggestions/{attributeId}", Name = AdminRouteNames.GetAttributeValueSuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        public IActionResult AttributeValueSuggestions(int attributeId)
        {
            if (attributeId <= 0)
            {
                return R.Fail.Result;
            }
            var attributes = _availableAttributeValueService.Get(x => x.AvailableAttributeId == attributeId);
            var model = new List<AutocompleteModel>();
            foreach (var c in attributes)
            {
                model.Add(new AutocompleteModel() {
                    Id = c.Id,
                    Text = c.Value
                });
            }
            return R.Success.With("suggestions", model.OrderBy(x => x.Text)).Result;
        }

    }
}