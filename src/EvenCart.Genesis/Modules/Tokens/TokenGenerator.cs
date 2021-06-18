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

using Genesis;
using Genesis.Extensions;

namespace EvenCart.Services.Orders
{
    [AutoResolvable]
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