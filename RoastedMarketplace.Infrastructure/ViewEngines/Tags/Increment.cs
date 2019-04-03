using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DotLiquid;
using DotLiquid.Exceptions;
using DotLiquid.Util;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Tags
{
    /// <summary>
    /// Increments an integer in your template. If not assigned initializes it from 0
    ///
    /// {% increment counter %}
    ///
    /// </summary>
    public class Increment : Tag
    {
        private static readonly Regex Syntax = R.B(R.Q(@"({0}+)\s*(.*)\s*"), Liquid.Expression);

        private Variable _from;

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            var syntaxMatch = Syntax.Match(markup);
            if (syntaxMatch.Success)
            {
                _from = new Variable(syntaxMatch.Groups[1].Value);
            }
            else
            {
                throw new SyntaxException("Increment tag exception");
            }

            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            var success = int.TryParse(context.Scopes.Last()[_from.Name]?.ToString(), out var value);
            context.Scopes.Last()[_from.Name] = success ? value + 1 : value;
        }
    }
}