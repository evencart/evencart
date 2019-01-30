using System.Collections.Generic;
using FluentValidation;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Plugins
{
    public class PluginInfoModel : FoundationModel, IRequiresValidations<PluginInfoModel>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string SystemName { get; set; }

        public string Author { get; set; }

        public string AuthorUri { get; set; }

        public string PluginUri { get; set; }

        public string AssemblyName { get; set; }

        public bool Installed { get; set; }

        public bool Active { get; set; }

        public string ConfigurationUrl{ get; set; }

        public void SetupValidationRules(ModelValidator<PluginInfoModel> v)
        {
            v.RuleFor(x => x.SystemName).NotEmpty();
        }
    }
}