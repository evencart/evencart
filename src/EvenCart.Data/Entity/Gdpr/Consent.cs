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

using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Gdpr
{
    public class Consent : FoundationEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPluginSpecificConsent { get; set; }

        public string PluginSystemName { get; set; }

        public bool IsRequired { get; set; }

        public int DisplayOrder { get; set; }

        public string LanguageCultureCode { get; set; }

        public bool EnableLogging { get; set; }

        public bool OneTimeSelection { get; set; }

        public bool Published { get; set; }

        public int ConsentGroupId { get; set; }

        #region Virtual Properties
        public virtual ConsentGroup ConsentGroup { get; set; }
        #endregion
    }
}