using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class StoreImplementation : FoundationModel
    {
        public string SoftwareVersion { get; set; }

        public string SoftwareTitle { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public ThemeImplementation Theme { get; set; }

        public string LogoUrl { get; set; }

        public string FaviconUrl { get; set; }

        public string CurrentPage { get; set; }

        public string CurrentUrl { get; set; }

        public string AffiliateUrl { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public bool WishlistEnabled { get; set; }

        public bool RepeatOrdersEnabled { get; set; }

        public bool ReviewsEnabled { get; set; }

        public bool ReviewModificationAllowed { get; set; }

        public bool RelatedProductsEnabled { get; set; }

        public bool ChangeCurrencyEnabled { get; set; }

        public string ActiveCurrencyCode { get; set; }

        public bool ShowCookieConsent { get; set; }

        public bool UseConsentGroup { get; set; }

        public string CookiePopupText { get; set; }

        public ConsentGroupImplementation ConsentGroup { get; set; }

        public string XsrfToken { get; set; }

        public string PrimaryCurrencyCode { get; set; }

        public string HoneypotFieldName { get; set; }

        public string ActiveCurrencySymbol { get; set; }

        public bool VendorSignupEnabled { get; set; }

        public bool VendorsEnabled { get; set; }
    }
}