using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class BundleExpander : Expander
    {
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var bundleMatches = regEx.Matches(inputContent);
            if (!bundleMatches.Any())
                return inputContent;
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            foreach (Match match in bundleMatches)
            {
                ExtractMatch(match, out _, out var keyValuePairs);
                if (keyValuePairs == null)
                {
                    throw new Exception($"The bundle tag must have a render parameter set to either 'css' or 'js' in file " + readFile.FileName);
                }
                keyValuePairs.TryGetValue("render", out var render);
                keyValuePairs.TryGetValue("bundle", out var bundle);
                bundle = bundle ?? "";
                render = render?.ToLower();
                if(render.IsNullEmptyOrWhiteSpace() || (render != "css" && render != "js"))
                    throw new Exception($"The bundle tag must have a render parameter set to either 'css' or 'js' in file " + readFile.FileName);

                if (render == "css")
                {
                    if (generalSettings.EnableCssBundling)
                    {
                        var cssBundle = readFile.GetMeta(nameof(CssExpander)).FirstOrDefault(x => x.Key == CssExpander.BundleKey);
                        var bundleUrls = (Dictionary<string, string>)cssBundle.Value;
                        if (!bundleUrls.ContainsKey(bundle))
                            throw new Exception($"The bundle with name '{bundle}' was not declared. File name " +
                                                readFile.FileName);
                        var bundleUrl = bundleUrls[bundle];
                        var link = $"<link rel=\"stylesheet\" href=\"{bundleUrl}\" />";
                        inputContent = inputContent.ReplaceFirstOccurance(match.Result("$0"), link);
                        readFile.Content = readFile.Content.ReplaceFirstOccurance(match.Result("$0"), link);
                    }
                    else
                    {
                        inputContent = inputContent.ReplaceFirstOccurance(match.Result("$0"), "");
                        readFile.Content = readFile.Content.ReplaceFirstOccurance(match.Result("$0"), "");
                    }
                    
                }
                else
                {
                    if (generalSettings.EnableJsBundling)
                    {
                        var jsBundle = readFile.GetMeta(nameof(JsExpander)).FirstOrDefault(x => x.Key == JsExpander.BundleKey);
                        var bundleUrls = (Dictionary<string, string>)jsBundle.Value;
                        if (!bundleUrls.ContainsKey(bundle))
                            throw new Exception($"The bundle with name '{bundle}' was not declared. File name " +
                                                readFile.FileName);
                        var bundleUrl = bundleUrls[bundle];
                        var script = $"<script type=\"text/javascript\" src=\"{bundleUrl}\"></script>";
                        inputContent = inputContent.ReplaceFirstOccurance(match.Result("$0"), script);
                        readFile.Content = readFile.Content.ReplaceFirstOccurance(match.Result("$0"), script);
                    }
                    else
                    {
                        inputContent = inputContent.ReplaceFirstOccurance(match.Result("$0"), "");
                        readFile.Content = readFile.Content.ReplaceFirstOccurance(match.Result("$0"), "");
                    }
                   
                }
            }

            return inputContent;
        }
    }
}