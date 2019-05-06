using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Installation
{
    public class InstallationRequestModel : FoundationModel, IRequiresValidations<InstallationRequestModel>
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

        public bool InstallSampleData { get; set; }

        public string StoreName { get; set; }

        public string ProviderName { get; set; }

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