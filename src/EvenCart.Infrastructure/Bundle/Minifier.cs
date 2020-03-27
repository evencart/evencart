#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
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
        private static readonly Regex CommentRegex = new Regex(@"(?=<!--)([\s\S]*?)-->", RegexOptions.Compiled);

        private const string IgnoreReplacement = "[IGNORE_SCRIPT]";

        public string MinifyHtml(string html)
        {
            if (html.IsNullEmptyOrWhiteSpace())
                return html;

            //remove the html comments
            html = CommentRegex.Replace(html, string.Empty);
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