using System;
using System.Collections;
using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure;
using EvenCart.Services.Plugins;
using Address = EvenCart.Data.Entity.Addresses.Address;
using Shippo;
using Shippo.Models;
using System.Linq;
using Shipment = Shippo.Models.Shipment;
using EvenCart.Data.Entity.Addresses;

namespace Shipping.Shippo
{
    public class ProviderPlugin : FoundationPlugin, IShipmentHandlerPlugin
    {
        private const int DEFAULT_TIMEOUT = 10;

        private readonly Settings _shippoSettings;
        private string API_KEY;

        public ProviderPlugin(Settings ShippoSettings)
        {
            _shippoSettings = ShippoSettings;
            API_KEY = _shippoSettings.DebugMode ? _shippoSettings.TestApiKey : _shippoSettings.LiveApiKey;
        }

        public decimal GetShippingHandlerFee(Cart cart)
        {
            APIResource resource = new APIResource(API_KEY);

            var products = cart.CartItems.Select(x => x.Product).ToList();
            var shipperInfo = cart.BillingAddress;
            var receiverInfo = cart.ShippingAddress;

            var shipmentTable = CreateShipmentConfig(products, shipperInfo, receiverInfo);

            // create Shipment object
            Shipment shipment = resource.CreateShipment(shipmentTable);

            if (shipment.Rates.Length == 0)
                return 0;

            return shipment.Rates[0].Amount;
        }

        public bool IsMethodAvailable(Cart cart)
        {
            return true;
        }

        public IList<ShippingOption> GetAvailableOptions(IList<Product> products, Address shipperInfo, Address receiverInfo)
        {
            var shippingOptions = new List<ShippingOption>();

            APIResource resource = new APIResource(API_KEY);

            var shipmentTable = CreateShipmentConfig(products, shipperInfo, receiverInfo);

            var shipment = resource.CreateShipment(shipmentTable);

            foreach (Rate rate in shipment.Rates)
            {
                shippingOptions.Add(new ShippingOption
                {
                    Rate = Convert.ToDecimal(rate.Amount),
                    DeliveryTime = rate.EstimatedDays?.ToString(),
                    Description = rate.DurationTerms.ToString(),
                    Name = rate.Servicelevel.Name.ToString(),
                    Remarks = rate.Servicelevel.Terms.ToString(),
                    Id = rate.ObjectId,
                });
            }

            return shippingOptions;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(ProviderConfig.ShippoProviderSettingsRouteName);

        #region Private 
        private Hashtable CreateShipmentConfig(IList<Product> products, Address shipperInfo, Address receiverInfo)
        {
            Hashtable toAddressTable = new Hashtable();
            toAddressTable.Add("name", shipperInfo.Name);
            toAddressTable.Add("street1", shipperInfo.Address1);
            toAddressTable.Add("city", shipperInfo.City);
            toAddressTable.Add("zip", shipperInfo.ZipPostalCode);
            toAddressTable.Add("country", shipperInfo.Country.Code);
            toAddressTable.Add("state", shipperInfo.StateProvinceName);
            toAddressTable.Add("phone", shipperInfo.Phone);
            toAddressTable.Add("email", shipperInfo.Email);

            // from address
            Hashtable fromAddressTable = new Hashtable();
            fromAddressTable.Add("name", receiverInfo.Name);
            fromAddressTable.Add("street1", receiverInfo.Address1);
            fromAddressTable.Add("city", receiverInfo.City);
            fromAddressTable.Add("zip", receiverInfo.ZipPostalCode);
            fromAddressTable.Add("country", receiverInfo.Country.Code);
            fromAddressTable.Add("state", receiverInfo.StateProvinceName);
            fromAddressTable.Add("phone", receiverInfo.Phone);
            fromAddressTable.Add("email", receiverInfo.Email);
            fromAddressTable.Add("metadata", receiverInfo.Id);
            //fromAddressTable.Add("is_residential", receiverInfo.AddressType == AddressType.Home);

            // parcel
            List<Hashtable> parcels = new List<Hashtable>();
            foreach (var product in products)
            {
                Hashtable parcelTable = new Hashtable();

                parcelTable.Add("name", product.Name);
                parcelTable.Add("length", product.PackageLength);
                parcelTable.Add("width", product.PackageWidth);
                parcelTable.Add("height", product.PackageHeight);
                parcelTable.Add("distance_unit", Helper.GetDistanceUnit(product.PackageLengthUnit));
                parcelTable.Add("weight", product.PackageWeight);
                parcelTable.Add("mass_unit", Helper.GetMassUnit(product.PackageWeightUnit));

                parcels.Add(parcelTable);
            }

            // shipment
            Hashtable shipmentTable = new Hashtable();
            shipmentTable.Add("address_to", toAddressTable);
            shipmentTable.Add("address_from", fromAddressTable);
            shipmentTable.Add("parcels", parcels);
            shipmentTable.Add("object_purpose", "PURCHASE");
            shipmentTable.Add("async", false);

            return shipmentTable;
        }
        #endregion
    }
}
