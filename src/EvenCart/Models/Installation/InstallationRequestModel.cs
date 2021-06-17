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

using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Models.Installation
{
    public class InstallationRequestModel : GenesisModel, IRequiresValidations<InstallationRequestModel>
    {
        public string ConnectionString { get; set; }

        public string AdminEmail { get; set; }
        
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }

        public string DatabaseName { get; set; }

        public string ServerUrl { get; set; }

        public bool CreateDatabaseIfNotExist { get; set; }

        public string DatabaseUserName { get; set; }

        public string DatabasePassword { get; set; }

        public bool IntegratedSecurity { get; set; }

        public bool IsConnectionString { get; set; }

        public string StoreName { get; set; }

        public string ProviderName { get; set; }

        public bool InstallSampleData { get; set; }

        public void SetupValidationRules(ModelValidator<InstallationRequestModel> v)
        {
            v.RuleFor(x => x.AdminEmail).NotEmpty().EmailAddress();
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
            v.RuleFor(x => x.DatabaseName).NotEmpty().When(x => !x.IsConnectionString);
            v.RuleFor(x => x.ServerUrl).NotEmpty().When(x => !x.IsConnectionString);
            v.RuleFor(x => x.StoreName).NotEmpty();
            v.RuleFor(x => x.ProviderName).NotEmpty().Must(x => x == "MySql" || x == "SqlServer");
        }
    }
}