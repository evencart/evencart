using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    [Route("[controller]")]
    public abstract class FoundationController : Controller
    {
        protected IActionResult Result(object model = null)
        {
            //ignore the view and return the model as json
            return Json(model);
        }

        protected IActionResult Result(string viewName, object model = null)
        {
            if (RequestHelper.IsApiCall())
            {
                //ignore the view and return the model as json
                return Json(model);
            }
            return View(viewName, model);
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
      
    }
}