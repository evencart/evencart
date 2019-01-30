using System.Collections.Generic;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Plugins
{
    public class WidgetModel : FoundationModel, IRequiresValidations<WidgetModel>
    {
        public string ZoneName { get; set; }

        public string WidgetName { get; set; }

        public string WidgetSystemName { get; set; }

        public string PluginName { get; set; }

        public string PluginSystemName { get; set; }

        public int DisplayOrder { get; set; }

        public IList<string> WidgetZones { get; set; }

        public string Id { get; set; }

        public bool HasConfiguration { get; set; }

        public string ConfigurationUrl { get; set; }

        public void SetupValidationRules(ModelValidator<WidgetModel> v)
        {
            v.RuleFor(x => x.ZoneName).NotEmpty();
            v.RuleFor(x => x.WidgetSystemName).NotEmpty();
            v.RuleFor(x => x.PluginSystemName).NotEmpty();
        }
    }
}