using System;
using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "Pagination")]
    public class PaginationComponent : FoundationComponent
    {
        private const int MaxVisiblePages = 10;

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var current =  int.Parse(dataAsDict["current"].ToString());
            var countPerPage = int.Parse(dataAsDict["rowCount"].ToString());
            var totalRecords = int.Parse(dataAsDict["total"].ToString());
            if (countPerPage == 0)
                return R.Success.ComponentResult;

            var totalPages = (int) Math.Ceiling((decimal) totalRecords / countPerPage);
            if (!dataAsDict.TryGetValue("callback", out object callbackObj))
            {
                throw new Exception("A callback function must be specified for pagination");
            }

            var functionName = callbackObj.ToString();
            var start = current - (int) MaxVisiblePages / 2;
            if (start <= 0)
                start = 1;
            var end = current + (int) MaxVisiblePages / 2 - 1;
            if (end > totalPages)
                end = totalPages;
            //1 2 3 4 [5] 6 7 8 9 10
            return R.Success.WithGridResponse(totalPages, current, 0).With("callback", functionName)
                .With("pageStart", start).With("pageEnd", end).ComponentResult;
        }
    }
}