using System.Text.RegularExpressions;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.Bundle
{
    public class Minifier : IMinifier
    {
        private static readonly Regex IgnoreScriptTagsRegex = new Regex(@"<(textarea|pre)[^>]*>[\w|\t|\r|\W]*?</(textarea|pre)>", RegexOptions.Compiled);
        private static readonly Regex ScriptTagsRegex = new Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>", RegexOptions.Compiled);
        private static readonly Regex TagsRegex = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])", RegexOptions.Compiled);
        private static readonly Regex BodyRegex = new Regex(@"</body>", RegexOptions.Compiled);

        private const string IgnoreReplacement = "[IGNORE_SCRIPT]";

        public string MinifyHtml(string html)
        {
            if (html.IsNullEmptyOrWhiteSpace())
                return html;

            var scriptMatchesToIgnore = IgnoreScriptTagsRegex.Matches(html);
            html = IgnoreScriptTagsRegex.Replace(html, IgnoreReplacement);

            var matches = ScriptTagsRegex.Matches(html);
            html = ScriptTagsRegex.Replace(html, string.Empty);
            html = TagsRegex.Replace(html, string.Empty);

            var scripts = string.Empty;
            foreach (Match match in matches)
            {
                scripts += match.ToString();
            }
            //replace the ignored tags again
            for (var i = 0; i < scriptMatchesToIgnore.Count; i++)
            {
                html = html.ReplaceFirstOccurance(IgnoreReplacement, scriptMatchesToIgnore[i].Value);
            }
            if (BodyRegex.IsMatch(html))
                html = BodyRegex.Replace(html, scripts + "</body>");
            else
                html = html + scripts;
            return html;
        }
    }
}