using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Dialog
{
    public class DialogModel : FoundationModel
    {
        public string EntityName { get; set; }

        public bool MultiSelect { get; set; }
    }
}