using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Promotions
{
    public class RestrictionValueModel : FoundationModel
    {
        public string Name { get; set; }

        public string RestrictionIdentifier { get; set; }
    }
}