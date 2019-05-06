using RoastedMarketplace.Infrastructure.Mvc.Widgets;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    public abstract class FoundationWidget
    {
        public abstract ComponentResult Invoke(object parameters = null);

        public ComponentResult WidgetView(object parameters = null)
        {
            return new ComponentResult()
            {
                Model = parameters
            };
        }
    }
}