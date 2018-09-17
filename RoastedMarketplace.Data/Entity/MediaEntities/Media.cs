using System;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Data.Interfaces;

namespace RoastedMarketplace.Data.Entity.MediaEntities
{
    public class Media : FoundationEntity, IUserResource
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string Description { get; set; }

        public string AlternativeText { get; set; }

        public string LocalPath { get; set; }

        public string ThumbnailPath { get; set; }

        public string MimeType { get; set; }

        public MediaType MediaType { get; set; }

        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
    }
}