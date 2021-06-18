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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Genesis.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Bundle;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class JsExpander : Expander
    {
        public const string BundleKey = "JSBundle";

        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var jsMatches = regEx.Matches(inputContent);
            if (!jsMatches.Any())
                return inputContent;
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            var jsFiles = new Dictionary<string, List<string>>();
            foreach (Match match in jsMatches)
            {
                ExtractMatch(match, out var parts, out var keyValuePairs);
                if (!parts.Any())
                    throw new Exception("No file provided for bundling");
                var bundle = "";
                keyValuePairs?.TryGetValue("bundle", out bundle);

                if (jsFiles.ContainsKey(bundle))
                    jsFiles[bundle] = jsFiles[bundle].Concat(parts).ToList();
                else
                {
                    jsFiles.Add(bundle, parts.ToList());
                }
            }

            var canBundle = generalSettings.EnableJsBundling && jsFiles.Any();
            string bundleUrl = null;
            Dictionary<string, string> bundleUrls = null;
            if (canBundle)
            {
                bundleUrls = new Dictionary<string, string>();
                var bundleService = DependencyResolver.Resolve<IBundleService>();
                foreach (var jsFile in jsFiles)
                {
                    bundleUrl = bundleService.GenerateJsBundle(jsFile.Value.ToArray());
                    bundleUrls.Add(jsFile.Key, bundleUrl);
                }
            }

            if (bundleUrls != null)
            {
                inputContent = regEx.Replace(inputContent, "");
                readFile.Content = regEx.Replace(readFile.Content, "");
                //add to readfile to be picked up by the renderer tag
                readFile.AddMeta(BundleKey, bundleUrls, nameof(JsExpander));
            }
            else
            {
                var fileIndex = 0;
                //render the link tags on page
                var alljsFiles = jsFiles.Values.SelectMany(x => x).ToArray();
                foreach (Match match in jsMatches)
                {
                    var script = $"<script type=\"text/javascript\" src=\"{alljsFiles[fileIndex++]}\"></script>";
                    inputContent = inputContent.ReplaceFirstOccurance(match.Result("$0"), script);
                }
            }

            return inputContent;
        }
    }
}