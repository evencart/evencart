using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoastedMarketplace.Data.Attributes;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Tokens
{
    public class TokenProcessor : ITokenProcessor
    {
        public string ProcessTokens(string content, IList<Token> tokens)
        {
            var builder = new StringBuilder(content);
            builder = tokens.Aggregate(builder, (current, token) => current.Replace(token.TokenName, token.TokenValue));
            return builder.ToString();
        }

        public IList<Token> GetAvailableTokens<T>(T entity)
        {
            //should we find name only or name with values
            var nameOnly = entity == null;
            var entityType = nameOnly ? typeof(T) : entity.GetType();

            //we use reflection to find all the public fields that are marked with attribute TokenField
            var properties = entityType.GetProperties();
            var tokenProperties = properties.Where(x => !Attribute.IsDefined(x, typeof(NonTokenFieldAttribute))).ToArray();

            if (!tokenProperties.Any())
                return null;

            var tokens = new List<Token>();
            var entityName = entityType.Name;
            var tokenNameFormat = entityName.ToCamelCase() + ".{0}";
            foreach (var tp in tokenProperties)
            {
                var tokenFieldName = "{{" + string.Format(tokenNameFormat, tp.Name.ToCamelCase()) + "}}";
                var token = new Token(tokenFieldName, "");
                //we need value as well
                if (!nameOnly)
                {
                    token.TokenValue = tp.GetValue(entity)?.ToString() ?? string.Empty;
                }
                //add the token
                tokens.Add(token);
            }

            return tokens;
        }

        public string ProcessTokens<T>(string content, T entity)
        {
            var availableTokens = GetAvailableTokens(entity);
            return ProcessTokens(content, availableTokens);
        }

        public string ProcessAllTokens(string content, params object[] entities)
        {
            foreach (var entity in entities)
                content = ProcessTokens(content, entity);
            return content;
        }
    }
}
