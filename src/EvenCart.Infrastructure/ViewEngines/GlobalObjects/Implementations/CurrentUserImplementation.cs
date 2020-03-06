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
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class CurrentUserImplementation : FoundationModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsVisitor { get; set; }
        
        public bool IsVendor { get; set; }

        public bool IsImitator { get; set; }

        public string ImageUrl { get; set; }

        public IList<string> Capabilities { get; set; }
    }
}