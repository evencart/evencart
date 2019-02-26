using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Tokens
{
    public class TokenGenerator : ITokenGenerator
    {
        public string MakeToken(TemplateToken templateToken)
        {
            var template = templateToken.Template;
            if (template.IsNullEmptyOrWhiteSpace())
                return null;

            return template
                .Replace("{ID}", templateToken.Id.ToString())
                .Replace("{UID}", templateToken.UserId.ToString())
                .Replace("{YY}", templateToken.DateTime.ToString("yy"))
                .Replace("{YYYY}", templateToken.DateTime.ToString("yyyy"))
                .Replace("{MM}", templateToken.DateTime.ToString("MM"))
                .Replace("{DD}", templateToken.DateTime.ToString("dd"))
                .Replace("{ID:R}", templateToken.Id.ToString().Reverse())
                .Replace("{UID:R}", templateToken.UserId.ToString().Reverse())
                .Replace("{YY:R}", templateToken.DateTime.ToString("yy").Reverse())
                .Replace("{YYYY:R}", templateToken.DateTime.ToString("yyyy").Reverse())
                .Replace("{MM:R}", templateToken.DateTime.ToString("MM").Reverse())
                .Replace("{DD:R}", templateToken.DateTime.ToString("dd").Reverse());
        }
    }
}