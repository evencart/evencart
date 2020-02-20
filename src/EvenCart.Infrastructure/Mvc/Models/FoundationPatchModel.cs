using System.Collections.Generic;
using EvenCart.Core.DataStructures;

namespace EvenCart.Infrastructure.Mvc.Models
{
    public abstract class FoundationPatchModel : FoundationModel
    {
        /// <summary>
        /// Stores the patched fields with values
        /// </summary>
        public abstract Dictionary<string, Pair<object, bool>> PatchFields { get; set; }
    }
}