using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Promotions
{
    public class RestrictionValueModel : FoundationModel
    {
        public string Name { get; set; }

        public string RestrictionIdentifier { get; set; }
    }
}