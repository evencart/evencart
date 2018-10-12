using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Promotions;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Promotions;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class DiscountsController : FoundationAdminController
    {
        private readonly IDiscountCouponService _discountCouponService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public DiscountsController(IDiscountCouponService discountCouponService, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _discountCouponService = discountCouponService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }

        [DualGet("", Name = AdminRouteNames.DiscountsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageDiscounts)]
        [ValidateModelState(ModelType = typeof(DiscountSearchModel))]
        public IActionResult DiscountsList([FromQuery] DiscountSearchModel searchModel)
        {
            var discountCoupons = _discountCouponService.SearchDiscountCoupons(searchModel.SearchPhrase,
                out int totalMatches, searchModel.Current, searchModel.RowCount);

            var discountCouponModels = discountCoupons.Select(x => _modelMapper.Map<DiscountModel>(x)).ToList();
            return R.Success.With("discounts", () => discountCouponModels,
                    () => _dataSerializer.Serialize(discountCouponModels))
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
            _modelMapper.Map(discountModel, discount);
            _discountCouponService.InsertOrUpdate(discount);
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
    }
}