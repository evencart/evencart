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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DotLiquid;
using DotLiquid.Exceptions;
using DotLiquid.Util;

namespace EvenCart.Infrastructure.ViewEngines.Tags
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