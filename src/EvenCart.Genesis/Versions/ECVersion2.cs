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

using System;
using System.Collections.Generic;
using DotEntity;
using DotEntity.MySql;
using DotEntity.SqlServer;
using EvenCart.Genesis.Modules.Users;
using Genesis;
using Genesis.Data;
using Genesis.Database;
using Genesis.Modules.Users;
using Db = DotEntity.DotEntity.Database;

namespace EvenCart.Data.Versions
{
    public class ECVersion2 : GenesisVersion
    {
        public override void Upgrade(IDotEntityTransaction transaction)
        {
            base.Upgrade(transaction);
            
            if (DotEntityDb.Provider is SqlServerDatabaseProvider)
                Db.Query(
                    "\r\nINSERT INTO [UserProfile] (UserId,CompanyName,DateOfBirth,FatherHusbandName,FirstName,Gender,IsTaxExempt,LastName,MobileNumber,Name,NewslettersEnabled,ProfilePictureId,Remarks)\r\nSELECT Id,CompanyName,DateOfBirth,'',FirstName,'',IsTaxExempt,LastName,MobileNumber,Name,NewslettersEnabled,ProfilePictureId,Remarks FROM [User]",
                    null, transaction);
            else if (DotEntityDb.Provider is MySqlDatabaseProvider)
            {
                Db.Query(
                    "\r\nINSERT INTO `UserProfile` (UserId,CompanyName,DateOfBirth,FatherHusbandName,FirstName,Gender,IsTaxExempt,LastName,MobileNumber,Name,NewslettersEnabled,ProfilePictureId,Remarks)\r\nSELECT Id,CompanyName,DateOfBirth,'',FirstName,'',IsTaxExempt,LastName,MobileNumber,Name,NewslettersEnabled,ProfilePictureId,Remarks FROM `User`",
                    null, transaction);
            }

            //delete columns from user table
            Db.DropColumn<User>(nameof(UserProfile.CompanyName), transaction);
            Db.DropColumn<User>(nameof(UserProfile.DateOfBirth), transaction);
            Db.DropColumn<User>(nameof(UserProfile.FirstName), transaction);
            Db.DropColumn<User>(nameof(UserProfile.IsTaxExempt), transaction);
            Db.DropColumn<User>(nameof(UserProfile.LastName), transaction);
            Db.DropColumn<User>(nameof(UserProfile.MobileNumber), transaction);
            Db.DropColumn<User>(nameof(UserProfile.Name), transaction);
            Db.DropColumn<User>(nameof(UserProfile.NewslettersEnabled), transaction);
            Db.DropColumn<User>(nameof(UserProfile.ProfilePictureId), transaction);
            Db.DropColumn<User>(nameof(UserProfile.Remarks), transaction);
        }

        public override Type[] GetTables()
        {
            return new[]
            {
                typeof(UserProfile)
            };
        }

        public override Dictionary<Relation, bool> GetRelations()
        {
            return new Dictionary<Relation, bool>();
        }

        public override string VersionKey => "EvenCart.Version.2";
    }
}