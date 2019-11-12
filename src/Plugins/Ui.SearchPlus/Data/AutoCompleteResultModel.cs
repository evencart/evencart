using EvenCart.Infrastructure.Mvc.Models;

namespace Ui.SearchPlus.Data
{
    public class AutoCompleteResultModel : FoundationModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}