using System;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Common
{
    public class ApiKey : FoundationEntity
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}