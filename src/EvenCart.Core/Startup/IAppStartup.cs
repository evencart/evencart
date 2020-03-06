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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EvenCart.Core.Startup
{
    public interface IAppStartup
    {
        void ConfigureServices(IServiceCollection services, IHostingEnvironment hostingEnvironment);

        void Configure(IApplicationBuilder app);

        /// <summary>
        /// The priority of task. Lower means earlier in pipeline
        /// </summary>
        int Priority { get; }
    }
}