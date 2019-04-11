using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.Localization;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Filters
{
    public class TranslateFilter : Filter
    {
        public override string Convert(string input)
        {
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            return localizer.Localize(input);
        }
    }
}