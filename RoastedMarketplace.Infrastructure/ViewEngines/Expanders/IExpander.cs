using System.Text.RegularExpressions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public interface IExpander
    {
        string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null);
    }
}