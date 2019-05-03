using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EvenCart.Core.DataStructures;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Bundle;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class CssExpander : Expander
    {
        public const string BundleKey = "CSSBundle";

        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var cssMatches = regEx.Matches(inputContent);
            if (!cssMatches.Any())
                return inputContent;
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            var cssFiles = new Dictionary<string, List<string>>();
            foreach (Match match in cssMatches)
            {
                ExtractMatch(match, out var parts, out var keyValuePairs);
                if(!parts.Any())
                    throw new Exception("No file provided for bundling");
                var bundle = "";
                keyValuePairs?.TryGetValue("bundle", out bundle);

                if (cssFiles.ContainsKey(bundle))
                    cssFiles[bundle] = cssFiles[bundle].Concat(parts).ToList();
                else
                {
                    cssFiles.Add(bundle, parts.ToList());
                }
            }

            var canBundle = generalSettings.EnableCssBundling && cssFiles.Any();
            string bundleUrl = null;
            Dictionary<string, string> bundleUrls = null;
            if (canBundle)
            {
                bundleUrls = new Dictionary<string, string>();
                var bundleService = DependencyResolver.Resolve<IBundleService>();
                foreach (var cssFile in cssFiles)
                {
                    bundleUrl = bundleService.GenerateCssBundle(cssFile.Value.ToArray());
                    bundleUrls.Add(cssFile.Key, bundleUrl);
                }
            }

            if (bundleUrls != null)
            {
                inputContent = regEx.Replace(inputContent, "");
                readFile.Content = regEx.Replace(readFile.Content, "");
                //add to readfile to be picked up by the renderer tag
                readFile.AddMeta(BundleKey, bundleUrls, nameof(CssExpander));
            }
            else
            {
                var fileIndex = 0;
                //render the link tags on page
                var allCssFiles = cssFiles.Values.SelectMany(x => x).ToArray();
                foreach (Match match in cssMatches)
                {
                    var link = $"<link rel=\"stylesheet\" href=\"{allCssFiles[fileIndex++]}\" />";
                    inputContent = inputContent.ReplaceFirstOccurance(match.Result("$0"), link);
                }
            }
          
            return inputContent;
        }
    }
}