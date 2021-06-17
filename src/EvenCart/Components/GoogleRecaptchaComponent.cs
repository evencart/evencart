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

using EvenCart.Models.Components;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "GoogleRecaptcha")]
    public class GoogleRecaptchaComponent : GenesisComponent
    {
        private readonly SecuritySettings _securitySettings;
        public GoogleRecaptchaComponent(SecuritySettings securitySettings)
        {
            _securitySettings = securitySettings;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var model = new GoogleRecaptchaModel()
            {
                SiteKey = _securitySettings.SiteKey
            };
            return R.Success.With("googleCaptchaSettings", model).ComponentResult;
        }
    }
}