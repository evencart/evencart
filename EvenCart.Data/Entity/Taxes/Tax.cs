using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Taxes
{
    public class Tax : FoundationEntity
    {
        public string Name { get; set; }

        #region Virtual Properties
        public virtual IList<TaxRate> TaxRates { get; set; }
        #endregion
    }
}