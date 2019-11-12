using EvenCart.Core.Config;

namespace Ui.SearchPlus
{
    public class SearchPlusSettings : ISettingGroup
    {
        public int NumberOfAutoCompleteResults { get; set; }

        public bool ShowTermCategory { get; set; }

        public string SearchBoxId { get; set; }

        public string WidgetId { get; set; }
    }
}