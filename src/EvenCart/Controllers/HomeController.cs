using System.Threading.Tasks;
using EvenCart.Events;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class HomeController : FoundationController
    {
        [HttpGet("~/", Name = RouteNames.Home)]
        public async Task<IActionResult> Index()
        {
            return R.Success.Result;
        }

        [DualPost("~/contact-us", Name = RouteNames.ContactUs, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(ContactUsModel))]
        public IActionResult ContactUs(ContactUsModel requestModel)
        {
            RaiseEvent(NamedEvent.ContactUs, requestModel);
            return R.Success.Result;
        }

    }
}