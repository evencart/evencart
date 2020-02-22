using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Common;
using EvenCart.Areas.Administration.Models.Common;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Custom labels are reusable text labels available for various operations such as cancellation, returns, product tags etc.
    /// </summary>
    public class CustomLabelsController : FoundationAdminController
    {
        private readonly ICustomLabelService _customLabelService;
        private readonly ICustomLabelModelFactory _customLabelModelFactory;
        public CustomLabelsController(ICustomLabelService customLabelService, ICustomLabelModelFactory customLabelModelFactory)
        {
            _customLabelService = customLabelService;
            _customLabelModelFactory = customLabelModelFactory;
        }

        /// <summary>
        /// Get label suggestions based on the query parameter
        /// </summary>
        /// <param name="labelType"></param>
        /// <param name="q">The search string for query</param>
        /// <response code="200">A list of <see cref="AutocompleteModel">suggestion</see> objects as 'suggestions'</response>
        [DualGet("suggestions/{labelType}", Name = AdminRouteNames.GetCustomLabelSuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCustomLabels)]
        public IActionResult CustomLabelSuggestions(string labelType, string q = null)
        {
            if (labelType.IsNullEmptyOrWhiteSpace())
                return R.Fail.Result;
            q = q ?? "";
            var attributes = _customLabelService.Get(x => x.Type == labelType && x.Text.StartsWith(q));
            var model = new List<AutocompleteModel>();
            foreach (var c in attributes)
            {
                model.Add(new AutocompleteModel()
                {
                    Id = c.Id,
                    Text = c.Text
                });
            }
            return R.Success.With("suggestions", model.OrderBy(x => x.Text)).Result;
        }

        /// <summary>
        /// Gets the custom labels
        /// </summary>
        /// <param name="searchModel">The <see cref="CustomLabelSearchModel">search parameters</see> to filter results.</param>
        /// <response code="200">A list of <see cref="CustomLabelModel">label</see> objects as 'customLabels'</response>
        [DualGet("{labelType}", Name = AdminRouteNames.CustomLabelsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageCustomLabels)]
        public IActionResult CustomLabelsList(CustomLabelSearchModel searchModel)
        {
            var customLabels = _customLabelService.Get(new List<string>() { searchModel.LabelType }, out int totalResults, searchModel.Current, searchModel.RowCount);
            var models = customLabels.Select(_customLabelModelFactory.Create).ToList();

            return R.Success.With("customLabels", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("labelType", searchModel.LabelType)
                .WithParams(searchModel)
                .Result;
        }

        /// <summary>
        /// Gets a single custom label
        /// </summary>
        /// <param name="customLabelId">The id of custom label</param>
        /// <param name="labelType">The type of <see cref="CustomLabelType">label</see></param>
        /// <response code="200">The <see cref="CustomLabelModel">custom label</see> object as 'customLabel'</response>
        [DualGet("{customLabelId}/{labelType}", Name = AdminRouteNames.GetCustomLabel)]
        [CapabilityRequired(CapabilitySystemNames.ManageCustomLabels)]
        public IActionResult CustomLabelEditor(int customLabelId, string labelType)
        {
            var customLabel = customLabelId > 0 ? _customLabelService.Get(customLabelId) : new CustomLabel()
            {
                Type = labelType
            };
            if (customLabel == null || customLabel.Type != labelType)
                return NotFound();

            var model = _customLabelModelFactory.Create(customLabel);
            return R.Success.With("customLabel", model)
                .With("customLabelId", customLabelId)
                .With("labelType", labelType).Result;
        }

        /// <summary>
        /// Saves the custom label to the database
        /// </summary>
        /// <param name="customLabelModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("", Name = AdminRouteNames.SaveCustomLabel, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCustomLabels)]
        [ValidateModelState(ModelType = typeof(CustomLabelModel))]
        public IActionResult SaveCustomLabel(CustomLabelModel customLabelModel)
        {
            var customLabel = customLabelModel.Id > 0
                ? _customLabelService.Get(customLabelModel.Id)
                : new CustomLabel();
            if (customLabel == null)
                return NotFound();
            customLabel.Text = customLabelModel.Text;
            customLabel.Type = customLabelModel.LabelType;
            _customLabelService.InsertOrUpdate(customLabel);
            return R.Success.Result;
        }

        /// <summary>
        /// Deletes a custom label
        /// </summary>
        /// <param name="customLabelId">The id of the label to be deleted</param>
        /// <response code="200">A success response object</response>
        [DualPost("delete", Name = AdminRouteNames.DeleteCustomLabel, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCustomLabels)]
        public IActionResult DeleteCustomLabel(int customLabelId)
        {
            var customLabel = _customLabelService.Get(customLabelId);
            if (customLabel == null)
                return NotFound();

            _customLabelService.Delete(customLabel);
            return R.Success.Result;
        }

        /// <summary>
        /// Updates display order for labels
        /// </summary>
        /// <param name="customLabelModels"></param>
        /// <response code="200">A success response object</response>
        [DualPost("display-order", Name = AdminRouteNames.UpdateCustomLabelDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCustomLabels)]
        public IActionResult UpdateWarehouseDisplayOrder(CustomLabelModel[] customLabelModels)
        {
            if (customLabelModels == null)
                return BadRequest();
            //get category models with no-zero ids
            var validModels = customLabelModels.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validModels)
                {
                    _customLabelService.Update(new { DisplayOrder = model.DisplayOrder }, m => m.Id == model.Id,
                        transaction);
                }
            });
            return R.Success.Result;
        }
    }
}