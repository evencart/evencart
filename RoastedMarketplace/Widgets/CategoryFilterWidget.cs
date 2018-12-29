using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Widgets;

namespace RoastedMarketplace.Widgets
{
    public class CategoryFilterWidget : FoundationWidget
    {
        public override ComponentResult Invoke(object parameters = null)
        {
            return WidgetView(parameters);
        }
    }
}