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
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Areas.Administration.Factories.System;
using EvenCart.Areas.Administration.Models.System;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Installation;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Allows admin to retrieve system information
    /// </summary>
    public class SystemController : FoundationAdminController
    {
        private readonly IAboutModelFactory _aboutModelFactory;
        private readonly IInstallationService _installationService;
        public SystemController(IAboutModelFactory aboutModelFactory, IInstallationService installationService)
        {
            _aboutModelFactory = aboutModelFactory;
            _installationService = installationService;
        }

        /// <summary>
        /// Gets the details about the software such as version, loaded assemblies etc. and system information such as available RAM, system time etc.
        /// </summary>
        /// <response code="200">A <see cref="AboutModel">info</see> object as 'info'</response>
        [DualGet("", Name = AdminRouteNames.GetAbout)]
        public IActionResult About()
        {
            var model = _aboutModelFactory.Create();
            return R.Success.With("info", model).Result;
        }
     
        [DualGet("install", Name = AdminRouteNames.InstallSampleData)]
        public IActionResult InstallSampleData()
        {
            return R.Success.Result;
        }

        /// <summary>
        /// Handles zip file uploads containing sample data and installs it on the server database
        /// </summary>
        /// <response code="200">A success object</response>
        [DualPost("install", Name = AdminRouteNames.InstallSampleData, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SampleDataModel))]
        public async Task<IActionResult> InstallSampleData(SampleDataModel sampleData)
        {
            var fileBytes = await sampleData.MediaFile.GetBytesAsync();
            //install the package
            var success = _installationService.InstallSamplePackage(fileBytes);
            if (success)
                return R.Success.Result;
            return R.Fail.With("error", T("Failed to install the sample package")).Result;
        }
    }
}