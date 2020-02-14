using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Purchases;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1E : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<Shipment, string>(nameof(Shipment.ShippingLabelUrl), "", transaction);
            Db.AddColumn<Shipment, string>(nameof(Shipment.TrackingUrl), "", transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<Shipment>(nameof(Shipment.ShippingLabelUrl), transaction);
            Db.DropColumn<Shipment>(nameof(Shipment.TrackingUrl), transaction);
        }

        public string VersionKey => "EvenCart.Version.1E";
    }
}