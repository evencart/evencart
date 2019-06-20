using System.Text.RegularExpressions;

namespace EvenCart.Infrastructure.Bundle
{
    //copied from https://github.com/ServiceStack/ServiceStack/blob/fd6343899d4933d56042352993262128996d8a77/src/ServiceStack/Html/Minifiers.cs
    public class Minifier : IMinifier
    {
        static readonly Regex BetweenScriptTagsRegEx = new Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>", RegexOptions.Compiled);
        static readonly Regex BetweenTagsRegex = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}", RegexOptions.Compiled);
        static readonly Regex MatchBodyRegEx = new Regex(@"</body>", RegexOptions.Compiled);

        public string MinifyHtml(string html)
        {
            if (html == null)
                return html;

            var matches = BetweenScriptTagsRegEx.Matches(html);
            html = BetweenScriptTagsRegEx.Replace(html, string.Empty);
            html = BetweenTagsRegex.Replace(html, string.Empty);

            var str = string.Empty;
            foreach (Match match in matches)
            {
                str += match.ToString();
            }

            html = MatchBodyRegEx.Replace(html, str + "</body>");
            return html;
        }
    }
}