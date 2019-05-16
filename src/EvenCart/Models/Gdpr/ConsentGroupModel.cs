using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Gdpr
{
    public class ConsentGroupModel : FoundationEntityModel
    {
        /// <summary>
        /// The name of consent group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the group
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of <see cref="ConsentModel">consents</see> that belong to this group
        /// </summary>
        public IList<ConsentModel> Consents { get; set; }
    }
}