#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Linq;
using EvenCart.Areas.Administration.Models.Promotions;
using EvenCart.Core.Data;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Promotions;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class DiscountsController : FoundationAdminController
    {
        private readonly IDiscountCouponService _discountCouponService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ICategoryService _categoryService;
        private readonly IVendorService _vendorService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICategoryAccountant _categoryAccountant;
        private readonly IRestrictionValueService _restrictionValueService;
        public DiscountsController(IDiscountCouponService discountCouponService, IModelMapper modelMapper, IDataSerializer dataSerializer, IProductService productService, IUserService userService, IRoleService roleService, ICategoryService categoryService, IVendorService vendorService, IManufacturerService manufacturerService, ICategoryAccountant categoryAccountant, IRestrictionValueService restrictionValueService)
        {
            _discountCouponService = discountCouponService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _productService = productService;
            _userService = userService;
            _roleService = roleService;
            _categoryService = categoryService;
            _vendorService = vendorService;
            _manufacturerService = manufacturerService;
            _categoryAccountant = categoryAccountant;
            _restrictionValueService = restrictionValueService;
        }

        [DualGet("", Name = AdminRouteNames.DiscountsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageDiscounts)]
        [ValidateModelState(ModelType = typeof(DiscountSearchModel))]
        public IActionResult DiscountsList([FromQuery] DiscountSearchModel searchModel)
        {
            var discountCoupons = _discountCouponService.SearchDiscountCoupons(searchModel.SearchPhrase,
                out int totalMatches, searchModel.Current, searchModel.RowCount);

            var discountCouponModels = discountCoupons.Select(x => _modelMapper.Map<DiscountModel>(x)).ToList();
            return R.Success.With("discounts", discountCouponModels)
                .WithGridResponse(totalMatches, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveDiscount)]
        [CapabilityRequired(CapabilitySystemNames.ManageDiscounts)]
        [ValidateModelState(ModelType = typeof(DiscountModel))]
        public IActionResult SaveDiscount(DiscountModel discountModel)
        {
            var discount = discountModel.Id > 0 ? _discountCouponService.Get(discountModel.Id) : new DiscountCoupon();
            if (discount == null)
                return NotFound();
            discountModel.StartDate = discountModel.StartDate ?? DateTime.UtcNow;
            _modelMapper.Map(discountModel, discount);
            discount.StartDate = discountModel.StartDate.Value;
            _discountCouponService.InsertOrUpdate(discount);

            //update the restrictions
            var restrictionIdentifiers = discountModel.RestrictionValues?.Select(x => x.RestrictionIdentifier).ToList();
            _discountCouponService.SetRestrictionIdentifiers(discount.Id, restrictionIdentifiers);
            return R.Success.With("id", discount.Id).Result;
        }

        [DualGet("{discountId}", Name = AdminRouteNames.GetDiscount)]
        [CapabilityRequired(CapabilitySystemNames.ManageDiscounts)]
        public IActionResult DiscountEditor(int discountId)
        {
            var discount = discountId > 0 ? _discountCouponService.Get(discountId) : new DiscountCoupon();
            if (discount == null)
                return NotFound();

            var discountModel = _modelMapper.Map<DiscountModel>(discount);
            var availableCalculationTypes = SelectListHelper.GetSelectItemList<CalculationType>();
            var availableRestrictionTypes = SelectListHelper.GetSelectItemList<RestrictionType>();
            //fetch the additional data if it's an existing discount
            if (discount.Id > 0)
            {
                switch (discount.RestrictionType)
                {
                    case RestrictionType.Products:
                        var productIds = discount.RestrictionIds();
                        if (!productIds.Any())
                            break;
                        var products = _productService.Get(x => productIds.Contains(x.Id));
                        discountModel.RestrictionValues = products.Select(x => new RestrictionValueModel()
                            {
                                Name = x.Name,
                                RestrictionIdentifier = x.Id.ToString()
                            })
                            .ToList();
                        break;
                    case RestrictionType.Categories:
                        var categoryIds = discount.RestrictionIds();
                        if (!categoryIds.Any())
                            break;
                        var allCategories = _categoryService.GetFullCategoryTree()
                            .Where(x => categoryIds.Contains(x.Id));
                        discountModel.RestrictionValues = allCategories.Select(x => new RestrictionValueModel() {
                                Name = _categoryAccountant.GetFullBreadcrumb(x),
                                RestrictionIdentifier = x.Id.ToString()
                            })
                            .ToList();
                        break;
                    case RestrictionType.Users:
                        var userIds = discount.RestrictionIds();
                        if (!userIds.Any())
                            break;
                        var users = _userService.Get(x => userIds.Contains(x.Id));
                        discountModel.RestrictionValues = users.Select(x => new RestrictionValueModel() {
                                Name = $"{x.Name} ({x.Email})",
                                RestrictionIdentifier = x.Id.ToString()
                            })
                            .ToList();
                        break;
                    case RestrictionType.UserGroups:
                        break;
                    case RestrictionType.Roles:
                        var roleIds = discount.RestrictionIds();
                        if (!roleIds.Any())
                            break;
                        var roles = _roleService.Get(x => roleIds.Contains(x.Id));
                        discountModel.RestrictionValues = roles.Select(x => new RestrictionValueModel() {
                                Name = $"{x.Name}",
                                RestrictionIdentifier = x.Id.ToString()
                            })
                            .ToList();
                        break;
                    case RestrictionType.Vendors:
                        var vendorIds = discount.RestrictionIds();
                        if (!vendorIds.Any())
                            break;
                        var vendors = _vendorService.Get(x => vendorIds.Contains(x.Id));
                        discountModel.RestrictionValues = vendors.Select(x => new RestrictionValueModel() {
                                Name = $"{x.Name}",
                                RestrictionIdentifier = x.Id.ToString()
                            })
                            .ToList();
                        break;
                    case RestrictionType.Manufacturers:
                        var manufacturerIds = discount.RestrictionIds();
                        if (!manufacturerIds.Any())
                            break;
                        var manufacturers = _vendorService.Get(x => manufacturerIds.Contains(x.Id));
                        discountModel.RestrictionValues = manufacturers.Select(x => new RestrictionValueModel() {
                                Name = $"{x.Name}",
                                RestrictionIdentifier = x.Id.ToString()
                            })
                            .ToList();
                        break;
                    case RestrictionType.PaymentMethods:
                        break;
                    case RestrictionType.ShippingMethods:
                        break;
                    case RestrictionType.OrderTotal:
                        break;
                    case RestrictionType.OrderSubTotal:
                        break;
                }
            }

            return R.Success.With("discount", discountModel)
                .With("availableCalculationTypes", availableCalculationTypes)
                .With("availableRestrictionTypes", availableRestrictionTypes)
                .Result;
        }

        [DualPost("{discountId}", Name = AdminRouteNames.DeleteDiscount)]
        [CapabilityRequired(CapabilitySystemNames.ManageDiscounts)]
        public IActionResult DeleteDiscount(int discountId)
        {
            if (discountId <= 0 || _discountCouponService.Count(x => x.Id == discountId) == 0)
                return NotFound();
            _discountCouponService.Delete(x => x.Id == discountId);
            return R.Success.Result;
        }

        [DualPost("restriction/delete", Name = AdminRouteNames.DeleteDiscountRestriction)]
        [CapabilityRequired(CapabilitySystemNames.ManageDiscounts)]
        public IActionResult DeleteRestriction(int discountId, string restrictionIdentifier)
        {
            if (discountId <= 0 || _discountCouponService.Count(x => x.Id == discountId) == 0)
                return NotFound();
            _restrictionValueService.Delete(x => x.DiscountCouponId == discountId &&
                                                 x.RestrictionIdentifier == restrictionIdentifier);

            return R.Success.Result;
        }
    }
}