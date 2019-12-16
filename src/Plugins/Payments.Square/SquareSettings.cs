using EvenCart.Core.Config;

namespace Payments.Square
{
    public class SquareSettings : ISettingGroup
    {
        public string ApplicationId { get; set; }

        public string AccessToken { get; set; }

        public bool EnableSandbox { get; set; }

        public string SandboxApplicationId { get; set; }

        public string SandboxAccessToken { get; set; }

        public bool AuthorizeOnly { get; set; }
       
        public bool UsePercentageForAdditionalFee { get; set; }

        public string LocationId { get; set; }
       
        public decimal AdditionalFee { get; set; }

        public string Description { get; set; }
    }
}