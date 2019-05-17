using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Available attributes are the attributes which are available for creation of product attributes. An available attribute can have multiple pre-defined values.
    /// </summary>
    public class AvailableAttributesController : FoundationAdminController
    {
        private readonly IAvailableAttributeService _availableAttributeService;
        private readonly IAvailableAttributeValueService _availableAttributeValueService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public AvailableAttributesController(IAvailableAttributeService availableAttributeService, IAvailableAttributeValueService availableAttributeValueService, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _availableAttributeService = availableAttributeService;
            _availableAttributeValueService = availableAttributeValueService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }

        /// <summary>
        /// Get attribute suggestions based on the query parameter
        /// </summary>
        /// <param name="q">The search string for query</param>
        /// <response code="200">A list of <see cref="AutocompleteModel">suggestion</see> objects</response>
        [DualGet("suggestions", Name = AdminRouteNames.GetAttributeSuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        public IActionResult AttributeSuggestions(string q = null)
        {
            q = q ?? "";
            var attributes = _availableAttributeService.Get(x => x.Name.StartsWith(q));
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
        /// <summary>
        /// Gets suggestions for values corresponding to an attribute
        /// </summary>
        /// <param name="attributeId">The id of the attribute</param>
        /// <response code="200">A list of <see cref="AutocompleteModel">suggestion</see> objects</response>
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

        /// <summary>
        /// Gets the available attributes
        /// </summary>
        /// <param name="searchModel">The <see cref="AttributeSearchModel">search parameters</see> to filter results.</param>
        /// <response code="200">A list of <see cref="AvailableAttributeModel">attribute</see> objects as 'attributes'</response>
        [DualGet("", Name = AdminRouteNames.AvailableAttributesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        public IActionResult AttributesList(AttributeSearchModel searchModel)
        {
            var attributes = _availableAttributeService.GetAvailableAttributes(out int totalResults,
                searchModel.SearchPhrase, searchModel.Current, searchModel.RowCount);

            var attributeModels = attributes.Select(GetAttributeModel).ToList();

            return R.Success.With("attributes", () => attributeModels, () => _dataSerializer.Serialize(attributeModels))
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .WithParams(searchModel)
                .Result;
        }

        /// <summary>
        /// Gets a single attribute
        /// </summary>
        /// <param name="availableAttributeId">The id of available attribute</param>
        /// <response code="200">The <see cref="AvailableAttributeModel">attribute</see> object with specified availableAttributeId</response>
        [DualGet("{availableAttributeId}", Name = AdminRouteNames.GetAvailableAttribute)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        public IActionResult AttributeEditor(int availableAttributeId)
        {
            var attribute = availableAttributeId > 0 ? _availableAttributeService.Get(availableAttributeId) : new AvailableAttribute();
            if (attribute == null)
                return NotFound();

            var model = GetAttributeModel(attribute);
            return R.Success.With("attribute", model).With("attributeId", availableAttributeId).Result;
        }

        /// <summary>
        /// Saves the available attribute to the database
        /// </summary>
        /// <param name="attributeModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("", Name = AdminRouteNames.SaveAvailableAttribute, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        [ValidateModelState(ModelType = typeof(AvailableAttributeModel))]
        public IActionResult SaveAvailableAttribute(AvailableAttributeModel attributeModel)
        {
            var attribute = attributeModel.Id > 0
                ? _availableAttributeService.Get(attributeModel.Id)
                : new AvailableAttribute();
            if (attribute == null)
                return NotFound();
            attribute.Name = attributeModel.Name;
            attribute.Description = attributeModel.Description;
            _availableAttributeService.InsertOrUpdate(attribute);

            Transaction.Initiate(transaction =>
            {
                //attribute values now
                var attributeValueModels = attributeModel.AttributeValues;
                foreach (var avm in attributeValueModels)
                {
                    if (!attribute.AvailableAttributeValues.Any(
                        x => x.Value.Equals(avm.Value, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        //insert this
                        _availableAttributeValueService.Insert(new AvailableAttributeValue() {
                            Value = avm.Value,
                            AvailableAttributeId = attribute.Id
                        }, transaction);
                    }
                }

                //remove the others
                foreach (var av in attribute.AvailableAttributeValues)
                {
                    if (!attributeValueModels.Any(x => x.Value.Equals(av.Value, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        _availableAttributeValueService.Delete(av, transaction);
                    }
                }
            });
            return R.Success.Result;
        }

        /// <summary>
        /// Deletes an available attribute
        /// </summary>
        /// <param name="attributeId">The id of the attribute to be deleted</param>
        /// <response code="200">A success response object</response>
        [DualPost("delete", Name = AdminRouteNames.DeleteAvailableAttribute, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageAvailableAttributes)]
        public IActionResult DeleteAvailableAttribute(int attributeId)
        {
            var attribute = _availableAttributeService.Get(attributeId);
            if (attribute == null)
                return NotFound();

            _availableAttributeService.Delete(attribute);
            return R.Success.Result;
        }

        #region Helpers

        private AvailableAttributeModel GetAttributeModel(AvailableAttribute attribute)
        {

            var model = _modelMapper.Map<AvailableAttributeModel>(attribute);
            model.AttributeValues = attribute.AvailableAttributeValues?
                .Select(y => _modelMapper.Map<AvailableAttributeValueModel>(y))
                .ToList();
            return model;
        }
        #endregion

    }
}