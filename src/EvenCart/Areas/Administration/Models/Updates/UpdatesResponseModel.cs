using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Updates
{
    public class UpdatesResponseModel
    {
        public int Current { get; set; }

        public int RowCount { get; set; }

        public int Total { get; set; }

        public int RangeStart { get; set; }

        public int RangeEnd { get; set; }

        public bool Success { get; set; }

        public IList<UpdateModel> FeedItems { get; set; }
    }
}