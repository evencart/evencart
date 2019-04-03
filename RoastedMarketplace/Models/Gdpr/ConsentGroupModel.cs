using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Gdpr
{
    public class ConsentGroupModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ConsentModel> Consents { get; set; }
    }
}