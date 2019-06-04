using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure;
using EvenCart.Services.Converters;
using EvenCart.Services.Plugins;

namespace Shipping.UPS
{
    public class UPSProviderPlugin : FoundationPlugin, IShipmentHandlerPlugin
    {
        private const int DEFAULT_TIMEOUT = 10;
        private const string DEVELOPMENT_RATES_URL = "https://wwwcie.ups.com/ups.app/xml/Rate";
        private const string PRODUCTION_RATES_URL = "https://onlinetools.ups.com/ups.app/xml/Rate";

        private readonly UPSSettings _upsSettings;
        private readonly IConverterService _converterService;
        public UPSProviderPlugin(UPSSettings upsSettings, IConverterService converterService)
        {
            _upsSettings = upsSettings;
            _converterService = converterService;
        }

        public decimal GetShippingHandlerFee(Cart cart)
        {
            return 0;   
        }

        public bool IsMethodAvailable(Cart cart)
        {
            return true;
        }

        public IList<ShippingOption> GetAvailableOptions(Cart cart, ShipperInfo shipperInfo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var url = _upsSettings.DebugMode ? DEVELOPMENT_RATES_URL : PRODUCTION_RATES_URL;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = DEFAULT_TIMEOUT * 1000;
            // Per the UPS documentation, the "ContentType" should be "application/x-www-form-urlencoded".
            // However, using "text/xml; encoding=UTF-8" lets us avoid converting the byte array returned by
            // the buildRatesRequestMessage method and (so far) works just fine.
            request.ContentType = "text/xml; encoding=UTF-8"; //"application/x-www-form-urlencoded";
            var bytes = BuildRatesRequestMessage(cart, shipperInfo);
            //System.Text.Encoding.Convert(Encoding.UTF8, Encoding.ASCII, this.buildRatesRequestMessage());
            request.ContentLength = bytes.Length;
            var stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            using (var resp = request.GetResponse() as HttpWebResponse)
            {
                if (resp != null && resp.StatusCode == HttpStatusCode.OK)
                {
                    var xDoc = XDocument.Load(resp.GetResponseStream());
                    return ParseRatesResponseMessage(xDoc);
                }
            }

            return null;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(UPSProviderConfig.UPSProviderSettingsRouteName);

        #region Private 
        private byte[] BuildRatesRequestMessage(Cart cart, ShipperInfo shipper)
        {
            Encoding utf8 = new UTF8Encoding(false);
            var writer = new XmlTextWriter(new MemoryStream(2000), utf8);
            writer.WriteStartDocument();
            writer.WriteStartElement("AccessRequest");
            writer.WriteAttributeString("lang", "en-US");
            writer.WriteElementString("AccessLicenseNumber", _upsSettings.LicenseNumber);
            writer.WriteElementString("UserId", _upsSettings.UserId);
            writer.WriteElementString("Password", _upsSettings.Password);
            writer.WriteEndDocument();
            writer.WriteStartDocument();
            writer.WriteStartElement("RatingServiceSelectionRequest");
            writer.WriteAttributeString("lang", "en-US");
            writer.WriteStartElement("Request");
            writer.WriteStartElement("TransactionReference");
            writer.WriteElementString("CustomerContext", "Rating and Service");
            writer.WriteElementString("XpciVersion", "1.0");
            writer.WriteEndElement(); // </TransactionReference>
            writer.WriteElementString("RequestAction", "Rate");
            writer.WriteElementString("RequestOption", "Shop");
            writer.WriteEndElement(); // </Request>
            writer.WriteStartElement("PickupType");
            writer.WriteElementString("Code", UPSHelper.GetPickupTypeCode(_upsSettings.PickupType));
            writer.WriteEndElement(); // </PickupType>
            writer.WriteStartElement("CustomerClassification");
            writer.WriteElementString("Code", UPSHelper.GetCustomerClassificationTypeCode(_upsSettings.CustomerClassificationType));

            writer.WriteEndElement(); // </CustomerClassification
            writer.WriteStartElement("Shipment");
            writer.WriteStartElement("Shipper");
            if (!string.IsNullOrWhiteSpace(_upsSettings.ShipperNumber))
            {
                writer.WriteElementString("ShipperNumber", _upsSettings.ShipperNumber);
            }
            writer.WriteStartElement("Address");
            writer.WriteElementString("PostalCode", shipper.ZipCode);
            writer.WriteElementString("CountryCode", shipper.CountryCode);
            writer.WriteEndElement(); // </Address>
            writer.WriteEndElement(); // </Shipper>
            writer.WriteStartElement("ShipTo");
            writer.WriteStartElement("Address");
            if (!string.IsNullOrWhiteSpace(cart.ShippingAddress.StateProvinceName))
            {
                writer.WriteElementString("StateProvinceCode", cart.ShippingAddress.StateProvinceName);
            }
            if (!string.IsNullOrWhiteSpace(cart.ShippingAddress.ZipPostalCode))
            {
                writer.WriteElementString("PostalCode", cart.ShippingAddress.ZipPostalCode);
            }
            writer.WriteElementString("CountryCode", cart.ShippingAddress.Country.Code);
            if (cart.ShippingAddress.AddressType == AddressType.Home)
            {
                writer.WriteElementString("ResidentialAddressIndicator", "true");
            }
            writer.WriteEndElement(); // </Address>
            writer.WriteEndElement(); // </ShipTo>
            writer.WriteStartElement("Service");
            writer.WriteElementString("Code", "03");
            writer.WriteEndElement(); //</Service>
           
            for (var i = 0; i < cart.CartItems.Count; i++)
            {
                var ci = cart.CartItems[i];
                writer.WriteStartElement("Package");
                writer.WriteStartElement("PackagingType");
                writer.WriteElementString("Code", "02");
                writer.WriteEndElement(); //</PackagingType>
                writer.WriteStartElement("PackageWeight"); //default is pounds
                writer.WriteElementString("Weight",
                    _converterService
                        .ConvertWeight(ci.Product.PackageWeightUnit, WeightUnit.Pound, ci.Product.PackageWeight)
                        .ToString());
                writer.WriteEndElement(); // </PackageWeight>
                writer.WriteStartElement("Dimensions");
                writer.WriteStartElement("UnitOfMeasurement");
                writer.WriteElementString("Code", "IN");
                writer.WriteElementString("Description", "Inches");
                writer.WriteEndElement(); //</UnitOfMeasurement>
                writer.WriteElementString("Length", ((int)_converterService
                    .ConvertLength(ci.Product.PackageLengthUnit, LengthUnit.Inch, ci.Product.PackageLength))
                    .ToString());
                writer.WriteElementString("Width", ((int) _converterService
                    .ConvertLength(ci.Product.PackageWidthUnit, LengthUnit.Inch, ci.Product.PackageWidth))
                    .ToString());
                writer.WriteElementString("Height", ((int)_converterService
                    .ConvertLength(ci.Product.PackageHeightUnit, LengthUnit.Inch, ci.Product.PackageHeight))
                    .ToString());
                writer.WriteEndElement(); // </Dimensions>
                writer.WriteStartElement("PackageServiceOptions");
                writer.WriteStartElement("InsuredValue");
                writer.WriteElementString("CurrencyCode", "USD");
                writer.WriteElementString("MonetaryValue", ci.Product.Price.ToString());
                writer.WriteEndElement(); // </InsuredValue>

                writer.WriteStartElement("DeliveryConfirmation");
                writer.WriteElementString("DCISType", "2");         // 2 represents Delivery Confirmation Signature Required
                writer.WriteEndElement(); // </DeliveryConfirmation>

                writer.WriteEndElement(); // </PackageServiceOptions>
                writer.WriteEndElement(); // </Package>
            }
            writer.WriteEndDocument();
            writer.Flush();
            var buffer = new byte[writer.BaseStream.Length];
            writer.BaseStream.Position = 0;
            writer.BaseStream.Read(buffer, 0, buffer.Length);
            writer.Close();
            var str = Encoding.ASCII.GetString(buffer);
            return buffer;
        }

        private IList<ShippingOption> ParseRatesResponseMessage(XDocument xDoc)
        {
            var shippingOptions = new List<ShippingOption>();
            if (xDoc.Root != null)
            {
                var ratedShipment = xDoc.Root.Elements("RatedShipment");
                foreach (var rateNode in ratedShipment)
                {
                    var name = rateNode.XPathSelectElement("Service/Code").Value;
                    if (!_upsSettings.ActiveServices.Contains(name))
                    {
                        continue;
                    }
                    var description = "";
                    var totalCharges = Convert.ToDecimal(rateNode.XPathSelectElement("TotalCharges/MonetaryValue").Value);
                    var delivery = DateTime.Parse("1/1/1900 12:00 AM");
                    var date = rateNode.XPathSelectElement("GuaranteedDaysToDelivery").Value;
                    if (date == "") // no gauranteed delivery date, so use MaxDate to ensure correct sorting
                    {
                        date = DateTime.MaxValue.ToShortDateString();
                    }
                    else
                    {
                        date = DateTime.Now.AddDays(Convert.ToDouble(date)).ToShortDateString();
                    }
                    var deliveryTime = rateNode.XPathSelectElement("ScheduledDeliveryTime").Value;
                    if (deliveryTime == "") // no scheduled delivery time, so use 11:59:00 PM to ensure correct sorting
                    {
                        date += " 11:59:00 PM";
                    }
                    else
                    {
                        date += " " + deliveryTime.Replace("Noon", "PM").Replace("P.M.", "PM").Replace("A.M.", "AM");
                    }
                    if (date != "")
                    {
                        delivery = DateTime.Parse(date);
                    }

                    shippingOptions.Add(new ShippingOption()
                    {
                        Name = name,
                        Description = description,
                        DeliveryTime = deliveryTime,
                        Rate = totalCharges
                    });
                }
            }

            return shippingOptions;
        }
        #endregion
    }
}
