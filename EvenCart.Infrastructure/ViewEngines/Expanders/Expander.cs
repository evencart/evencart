using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Database;
using EvenCart.Infrastructure.Localization;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public abstract class Expander : IExpander
    {
        public string TagName { get; set; }

        private Regex AssociatedRegEx { get; set; }

        private readonly ILocalizer _localizer;
        protected Expander()
        {
            _localizer = DependencyResolver.Resolve<ILocalizer>();
        }

        static Expander()
        {
            var dbInstalled = DatabaseManager.IsDatabaseInstalled();
            Expanders = new List<Expander>();
            //the sequence of addition is important
            Expanders.Add(new LayoutExpander() { TagName = "layout" });
            Expanders.Add(new PartialExpander() { TagName = "partial" });
            if (dbInstalled)
            {
                Expanders.Add(new WidgetExpander() { TagName = "widget" });
                Expanders.Add(new ComponentExpander() { TagName = "component" });
                Expanders.Add(new ControlExpander() { TagName = "control" });
            }
           
            Expanders.Add(new UrlRouteExpander() { TagName = "route" });

            if (dbInstalled)
            {
                Expanders.Add(new NavigationExpander() { TagName = ApplicationConfig.PrimaryNavigationName, NavigationType = ApplicationConfig.PrimaryNavigationName });
                Expanders.Add(new NavigationExpander() { TagName = ApplicationConfig.SecondaryNavigationName, NavigationType = ApplicationConfig.SecondaryNavigationName });
                Expanders.Add(new GlobalExpander() { TagName = "global" });
                Expanders.Add(new JsonExpander() { TagName = "json" });
                Expanders.Add(new CssExpander() { TagName = "css" });
                Expanders.Add(new JsExpander() { TagName = "js" });
                Expanders.Add(new BundleExpander() { TagName = "bundle" });
            }
         
        }
        private static readonly List<Expander> Expanders;

        private const string TagMatcherPattern =
            @"{%\s*{tagName}(?:\s+([\w\d._\\/]+|""[^""]+"")(?!\w*=))*(\s+[\w\d\s_]+(?>=(?:@t)?"")[^""]*(?="")"")*\s*%}";

        private static Regex GetTagRegex(string tagName)
        {
            return new Regex(TagMatcherPattern.Replace("{tagName}", tagName));
        }
        public static void ExpandView(string viewName, object parameters, out string expandedContent)
        {
            var readFile = ReadFile.From(viewName);
            expandedContent = SafeExpandView(readFile, readFile.Content, parameters, true);
        }

        private static string SafeExpandView(ReadFile readFile, string inputContent, object parameters = null, bool prePostRun = false)
        {
            foreach (var e in Expanders)
            {
                var tagRegex = e.AssociatedRegEx ?? GetTagRegex(e.TagName);
                e.AssociatedRegEx = tagRegex;
                if (prePostRun)
                    //run before expansion
                    e.PreRun(readFile);
                //run the expansion
                inputContent = e.Expand(readFile, tagRegex, inputContent, parameters);
                if (prePostRun)
                    //run the post expansion
                    e.PostRun(readFile);
            }

            //run the matchers again if required after expansion
            foreach (var e in Expanders)
            {
                if (e.AssociatedRegEx.Matches(inputContent).Any())
                    inputContent = SafeExpandView(readFile, inputContent, parameters);
            }
            return inputContent;
        }

        protected void ExtractMatch(Match match, out string[] straightParameters, out Dictionary<string, string> keyValuePairs)
        {
            straightParameters = null;
            keyValuePairs = null;

            if (match.Groups[1].Success)
            {
                straightParameters = match.Groups[1].Captures.Select(x => x.Value.Trim('"')).ToArray();
            }
            if (match.Groups[2].Success)
            {
                //let's populate dictionary with keyvalue pairs, stripping out everything that's not needed
                keyValuePairs = new Dictionary<string, string>();
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    var pSplit = capture.Value.Split('=', 2)./*Select(x => x.Trim('"')).*/ToList();
                    if (pSplit[1].StartsWith("@t"))
                    {
                        //the value needs a translation
                        pSplit[1] = _localizer.Localize(pSplit[1].Substring(2).Trim('"'));
                    }
                    else
                        pSplit[1] = pSplit[1].Trim('"');
                    pSplit[0] = pSplit[0].Trim();
                    keyValuePairs.Add(pSplit[0], pSplit[1]);
                }
            }
        }

        public abstract string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null);

        public virtual void PreRun(ReadFile readFile)
        {
            //execute anything that needs to be done before expansion
        }
        public virtual void PostRun(ReadFile readFile)
        {
            //execute anything that needs to be done before expansion
        }

    }
}