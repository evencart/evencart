using System.ComponentModel.DataAnnotations;
using RoastedMarketplace.Core.Infrastructure.Attributes;

namespace RoastedMarketplace.Core.Data
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
    }
}