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

using System.Linq;
using EvenCart.Areas.Administration.Models.Taxes;
using EvenCart.Data.Entity.Taxes;
using EvenCart.Genesis.Mvc;
using EvenCart.Services.Taxes;
using Genesis;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Modules.Data;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class TaxesController : GenesisAdminController
    {
        private readonly ITaxService _taxService;
        private readonly ITaxRateService _taxRateService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IModelMapper _modelMapper;
        public TaxesController(ITaxService taxService, ITaxRateService taxRateService, IDataSerializer dataSerializer, IModelMapper modelMapper)
        {
            _taxService = taxService;
            _taxRateService = taxRateService;
            _dataSerializer = dataSerializer;
            _modelMapper = modelMapper;
        }

        #region Tax
        [DualGet("", Name = AdminRouteNames.TaxesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        [ValidateModelState(ModelType = typeof(AdminSearchModel))]
        public IActionResult TaxesList(TaxSearchModel searchModel)
        {
            var taxes = _taxService.Get(out int totalResults, x => true, x => x.Name, page: searchModel.Current,
                count: searchModel.RowCount);

            var models = taxes.Select(x => _modelMapper.Map<TaxModel>(x)).ToList();
            return R.Success.WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("taxes", models)
                .WithParams(searchModel)
                .Result;
        }

        [DualGet("{taxId}", Name = AdminRouteNames.GetTax)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        public IActionResult TaxEditor(int taxId)
        {
            var tax = taxId > 0 ? _taxService.Get(taxId) : new Tax();
            if (tax == null)
                return NotFound();
            var taxModel = _modelMapper.Map<TaxModel>(tax);
            return R.Success.With("tax", taxModel).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveTax, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        [ValidateModelState(ModelType = typeof(TaxModel))]
        public IActionResult SaveTax(TaxModel taxModel)
        {
            var tax = taxModel.Id > 0 ? _taxService.Get(taxModel.Id) : new Tax();
            if (tax == null)
                return NotFound();

            tax.Name = taxModel.Name;
            _taxService.InsertOrUpdate(tax);
            return R.Success.Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteTax, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        public IActionResult DeleteTax(int taxId)
        {
            var tax = taxId > 0 ? _taxService.Get(taxId) : null;
            if (tax == null)
                return NotFound();

            _taxService.Delete(tax);
            return R.Success.Result;
        }
        #endregion

        #region TaxRate Rate
        [DualGet("{taxId}/taxrates", Name = AdminRouteNames.TaxRatesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        public IActionResult TaxRatesList(int taxId)
        {
            Tax tax = null;
            if (taxId <= 0 || (tax = _taxService.FirstOrDefault(x => x.Id == taxId)) == null)
                return NotFound();
            var taxRates = _taxRateService.Get(x => x.TaxId == taxId);

            var models = taxRates.Select(x =>
            {
                var model = _modelMapper.Map<TaxRateModel>(x);
                model.CountryName = x.Country.Name;
                model.StateOrProvinceName = x.StateOrProvince?.Name;
                return model;
            }).ToList();

            return R.Success.With("taxRates", models)
                .WithGridResponse(models.Count, 1, models.Count)
                .With("taxId", taxId)
                .With("taxName", tax.Name)
                .WithAvailableCountries()
                .Result;
        }

        [DualGet("{taxId}/taxrates/{taxRateId}", Name = AdminRouteNames.GetTaxRate)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        public IActionResult TaxRateEditor(int taxId, int taxRateId)
        {
            if (taxId <= 0 || _taxService.Count(x => x.Id == taxId) == 0)
                return NotFound();

            var taxRate = taxRateId > 0 ? _taxRateService.Get(taxRateId) : new TaxRate()
            {
                TaxId = taxId
            };
            if (taxRate == null)
                return NotFound();
            var taxRateModel = _modelMapper.Map<TaxRateModel>(taxRate);
            return R.Success.With("taxRate", taxRateModel).WithAvailableCountries().Result;
        }

        [DualPost("{taxId}/taxrates", Name = AdminRouteNames.SaveTaxRate, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        [ValidateModelState(ModelType = typeof(TaxRateModel))]
        public IActionResult SaveTaxRate(TaxRateModel taxRateModel)
        {
            if (taxRateModel.TaxId <= 0 || _taxService.Count(x => x.Id == taxRateModel.TaxId) == 0)
                return NotFound();

            var taxRate = taxRateModel.Id > 0 ? _taxRateService.Get(taxRateModel.Id) : new TaxRate();
            if (taxRate == null)
                return NotFound();

            _modelMapper.Map(taxRateModel, taxRate);
            _taxRateService.InsertOrUpdate(taxRate);
            return R.Success.Result;
        }

        [DualPost("taxrates/delete", Name = AdminRouteNames.DeleteTaxRate, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageTaxes)]
        public IActionResult DeleteTaxRate(int taxRateId)
        {
            var taxRate = taxRateId > 0 ? _taxRateService.Get(taxRateId) : null;
            if (taxRate == null)
                return NotFound();

            _taxRateService.Delete(taxRate);
            return R.Success.Result;
        }
        #endregion

    }
}