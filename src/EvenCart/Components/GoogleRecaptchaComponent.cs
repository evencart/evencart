using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "GoogleRecaptcha")]
    public class GoogleRecaptchaComponent : FoundationComponent
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