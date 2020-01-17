using System;
using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1B : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<StoreCredit>(transaction);
            Db.CreateConstraint(Relation.Create<User, StoreCredit>("Id", "UserId"), transaction, true);

            Db.AddColumn<User, bool>(nameof(User.IsAffiliate), true, transaction);
            Db.AddColumn<User, bool>(nameof(User.AffiliateActive), true, transaction);
            Db.AddColumn<User, DateTime?>(nameof(User.AffiliateFirstActivationDate), DateTime.UtcNow, transaction);
            Db.AddColumn<User, DateTime?>(nameof(User.FirstActivationDate), DateTime.UtcNow, transaction);
            Db.AddColumn<Order, bool>(nameof(Order.UsedStoreCredits), false, transaction);
            Db.AddColumn<Order, decimal>(nameof(Order.StoreCredits), 0, transaction);
            Db.AddColumn<Order, decimal>(nameof(Order.StoreCreditAmount), 0, transaction);
            Db.AddColumn<Cart, bool>(nameof(Cart.UseStoreCredits), false, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropConstraint(Relation.Create<User, StoreCredit>("Id", "UserId"), transaction);
            Db.DropTable<StoreCredit>(transaction);

            Db.DropColumn<User>(nameof(User.FirstActivationDate), transaction);
            Db.DropColumn<User>(nameof(User.IsAffiliate), transaction);
            Db.DropColumn<User>(nameof(User.AffiliateActive), transaction);
            Db.DropColumn<User>(nameof(User.AffiliateFirstActivationDate), transaction);
            Db.DropColumn<Order>(nameof(Order.UsedStoreCredits), transaction);
            Db.DropColumn<Order>(nameof(Order.StoreCredits), transaction);
            Db.DropColumn<Order>(nameof(Order.StoreCreditAmount), transaction);
            Db.DropColumn<Cart>(nameof(Cart.UseStoreCredits), transaction);
        }

        public string VersionKey => "EvenCart.Version.1B";
    }
}