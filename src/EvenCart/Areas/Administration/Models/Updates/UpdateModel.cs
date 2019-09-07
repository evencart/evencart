using System;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Updates
{
    /// <summary>
    /// Represents a news object
    /// </summary>
    public class UpdateModel : FoundationModel
    {
        /// <summary>
        /// The title of the update
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The summary of the update
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date when this update was published
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The target url of the update
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Specifies if update should be highlighted
        /// </summary>
        public bool Highlight { get; set; }
    }
}