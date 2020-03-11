#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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
                return R.Success.With("pageStart", 0).With("pageEnd", 0).WithGridResponse(0, current, countPerPage).ComponentResult;

            var totalPages = (int) Math.Ceiling((decimal) totalRecords / countPerPage);
            dataAsDict.TryGetValue("callback", out var callbackObj);
            dataAsDict.TryGetValue("url", out var url);
            dataAsDict.TryGetValue("maxPages", out var maxPagesStr);
            dataAsDict.TryGetValue("wrapperClass", out var wrapperClass);
            var maxPages = MaxVisiblePages;
            if (maxPagesStr != null)
                int.TryParse(maxPagesStr.ToString(), out maxPages);

            var functionName = callbackObj?.ToString() ?? "void";
            var start = current - (int) maxPages / 2;
            if (start <= 0)
                start = 1;
            var end = current + (int) maxPages / 2 - 1;
            if (end > totalPages)
                end = totalPages;
            //1 2 3 4 [5] 6 7 8 9 10
            return R.Success.WithGridResponse(totalPages, current, countPerPage).With("callback", functionName).With("url", url)
                .With("pageStart", start).With("pageEnd", end).With("wrapperClass", wrapperClass).ComponentResult;
        }
    }
}