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
using System.Linq;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.Mvc.UI
{
    public class MenuItem : FoundationModel
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public string SystemName { get; set; }

        public List<MenuItem> ChildItems { get; } = new List<MenuItem>();

        public string Icon { get; set; }

        public bool OpenInNewWindow { get; set; }

        private MenuItem Parent { get; set; }

        public MenuItem AddMenuItem(params MenuItem[] menuItem)
        {
            foreach (var mi in menuItem)
            {
                mi.Parent = this;
                ChildItems.Add(mi);
            }
            return this;
        }

        public MenuItem GetParent()
        {
            return Parent;
        }

        public MenuItem this[int index]
        {
            get
            {
                if (ChildItems.Any())
                    return ChildItems[index];
                return null;
            }
        }
    }
}