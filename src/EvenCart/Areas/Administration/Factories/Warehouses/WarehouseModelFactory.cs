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

using EvenCart.Areas.Administration.Factories.Addresses;
using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Areas.Administration.Factories.Warehouses
{
    public class WarehouseModelFactory : IWarehouseModelFactory
    {
        private readonly IAddressModelFactory _addressModelFactory;
        public WarehouseModelFactory(IAddressModelFactory addressModelFactory)
        {
            _addressModelFactory = addressModelFactory;
        }

        public WarehouseModel Create(Warehouse entity)
        {
            var model = new WarehouseModel()
            {
                Id = entity.Id,
                DisplayOrder = entity.DisplayOrder
            };
            if (entity.Address != null)
                model.Address = _addressModelFactory.Create(entity.Address);
            return model;
        }

        public WarehouseMiniModel  CreateMini(Warehouse entity)
        {
            var model = new WarehouseMiniModel()
            {
                Id = entity.Id,
                Name = entity.Address?.Name,
                DisplayOrder = entity.DisplayOrder
            };
            return model;
        }
    }
}