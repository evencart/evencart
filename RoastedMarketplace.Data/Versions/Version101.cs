using System;
using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<User, string>(nameof(User.CompanyName), null, transaction);
            Db.AddColumn<User, DateTime?>(nameof(User.DateOfBirth), null, transaction);
            Db.DropColumn<User>("UserName", transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }

    public class Version102 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<User, bool>(nameof(User.NewslettersEnabled), false, transaction);
            Db.AddColumn<User, bool>(nameof(User.IsTaxExempt), false, transaction);
            Db.AddColumn<User, bool>(nameof(User.RequirePasswordChange), false, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_2";
    }

    public class Version103 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<DiscountCoupon>(transaction);
            Db.CreateTable<RestrictionValue>(transaction);
            Db.CreateConstraint(Relation.Create<DiscountCoupon, RestrictionValue>("Id", "DiscountCouponId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_3";
    }
}