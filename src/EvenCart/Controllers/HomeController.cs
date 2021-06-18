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

using System.Threading.Tasks;
using EvenCart.Events;
using EvenCart.Models.Home;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class HomeController : GenesisController
    {
        [DualGet("~/", Name = RouteNames.Home, OnlyNonApi = true, AvailableInHeadlessMode = true)]
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