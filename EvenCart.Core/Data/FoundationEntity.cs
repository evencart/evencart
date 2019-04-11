using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EvenCart.Core.Infrastructure.Attributes;

namespace EvenCart.Core.Data
{
    public abstract partial class FoundationEntity
    {
        /// <summary>
        /// The Id of the entity
        /// </summary>
        [Key]
        [NonPatchable]
        public int Id { get; set; }
        
        public virtual object Tag { get; set; }

        internal virtual Dictionary<string, object> MetaData { get; set; }
    }
}