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
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Plugins
{
    public interface IShipmentHandlerPlugin : IPlugin
    {
        bool IsMethodAvailable(Cart cart);

        IList<ShippingOption> GetAvailableOptions(IList<(Product, int)> products, Address shipperInfo, Address receiverInfo);

        ShipmentInfo GetShipmentInfo(ShippingOption selectedShippingOption, IList<(Product, int)> products);

        bool SupportsLabelPurchase { get; }
    }
}