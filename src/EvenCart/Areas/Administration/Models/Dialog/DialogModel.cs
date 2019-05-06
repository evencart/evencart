using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Dialog
{
    public class DialogModel : FoundationModel
    {
        public string EntityName { get; set; }

        public bool MultiSelect { get; set; }
    }
}