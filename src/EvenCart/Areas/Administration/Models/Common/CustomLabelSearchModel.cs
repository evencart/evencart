using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Common
{
    /// <summary>
    /// The custom label search object
    /// </summary>
    public class CustomLabelSearchModel : AdminSearchModel
    {
        public string LabelType { get; set; }
    }
}