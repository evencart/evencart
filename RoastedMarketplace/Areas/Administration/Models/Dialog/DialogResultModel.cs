using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Dialog
{
    public class DialogResultModel : FoundationModel
    {
        public string ApiUrl { get; set; }

        public string UiUrl { get; set; }

        public string ResponseObjectName { get; set; }

        public bool MultiSelect { get; set; }

        public string DialogTitle { get; set; }

        public string DisplayField { get; set; }
    }
}