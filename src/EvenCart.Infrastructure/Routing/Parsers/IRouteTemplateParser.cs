using System.Collections.Generic;

namespace EvenCart.Infrastructure.Routing.Parsers
{
    public interface IRouteTemplateParser
    {
        Dictionary<string, string> ParsePathForTemplate(string path, string template);

        string GetProcessedRouteTemplate(string template, WrapType wrapType = WrapType.WholeString, bool escapeBrackets = false);
    }
}