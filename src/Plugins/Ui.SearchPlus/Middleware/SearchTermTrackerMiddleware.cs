using System.Linq;
using System.Threading.Tasks;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Http;
using Ui.SearchPlus.Data;
using Ui.SearchPlus.Services;

namespace Ui.SearchPlus.Middleware
{
    public class SearchTermTrackerMiddleware
    {
        private readonly RequestDelegate _next;
        private string _searchPageUrl = null;
        private readonly ISearchTermService _searchTermService;
        public SearchTermTrackerMiddleware(RequestDelegate next, ISearchTermService searchTermService)
        {
            _next = next;
            _searchTermService = searchTermService;
        }

        public async Task Invoke(HttpContext context)
        {
            _searchPageUrl = _searchPageUrl ?? "/s";
            //execute only if its search page requested
            if (context.Request.Path == _searchPageUrl)
            {
                var searchTerm = context.Request.Query["search"].FirstOrDefault();
                if (!searchTerm.IsNullEmptyOrWhiteSpace())
                {
                    //do the tracking
                    var term = _searchTermService.FirstOrDefault(x => x.Term == searchTerm) ?? new SearchTerm()
                    {
                        Term = searchTerm
                    };
                    term.Score++;
                    _searchTermService.InsertOrUpdate(term);
                }

            }
            await _next(context);
        }
    }
}