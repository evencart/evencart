using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Countries
{
    public class StateOrProvinceModel : FoundationEntityModel
    {
        /// <summary>
        /// The country id of the state
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// The name of the state
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The publish status of state
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// If shipping is available for the state or not
        /// </summary>
        public bool ShippingEnabled { get; set; }

        /// <summary>
        /// The display order of the state in the list
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}