using System;
using EvenCart.Core.Data;
using EvenCart.Data.Enum;
using EvenCart.Data.Interfaces;

namespace EvenCart.Data.Entity.MediaEntities
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

        public DateTime CreatedOn { get; set; }

        public int UserId { get; set; }

        public int DisplayOrder { get; set; }
    }
}