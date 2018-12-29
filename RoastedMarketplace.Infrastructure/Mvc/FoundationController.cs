using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Helpers;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    public abstract class FoundationController : Controller
    {
        protected IActionResult Result(object model = null)
        {
            if (RequestHelper.IsApiCall())
            {
                //ignore the view and return the model as json
                return Json(model);
            }
            return View(model);
        }

        protected IActionResult Result(IActionResult mvcResult, IActionResult apiResult)
        {
            return RequestHelper.IsApiCall() ? apiResult : mvcResult;
        }

        protected IActionResult ContentResult(string content)
        {
            if (RequestHelper.IsApiCall())
                return Json(new { content });
            return Content(content);
        }

        public string T(string resource, string languageCultureCode = "en-US", params object[] arguments)
        {
            return LocalizationHelper.Localize(resource, languageCultureCode, arguments);
        }

        public CustomResponse R => CustomResponse.Response(this);
        
    }
   
}