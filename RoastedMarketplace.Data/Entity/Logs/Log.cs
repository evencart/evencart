#region Author Information
// Log.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Data.Entity.Logs
{
    public class Log : FoundationEntity
    {
        public LogLevel LogLevel { get; set; }

        public string ShortMessage { get; set; }

        public string Details { get; set; }

        public string IpAddress { get; set; }

        public string Url { get; set; }

        public string ReferralUrl { get; set; }

        public DateTime DateCreated { get; set; }
    }

}