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
using System.IO;
using System.Linq;
using EvenCart.Genesis;
using EvenCart.Swagger;
using Genesis;
using Genesis.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EvenCart
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private IGenesisEngine _appEngine;

        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _appEngine = GenesisApp.Current.ConfigureEngine<ApplicationEngine>(services, _hostingEnvironment, _configuration);
            var applicationConfig = GenesisApp.Current.ApplicationConfig;
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvenCart Api Documentation", Version = applicationConfig.ApiVersion });
                c.CustomSchemaIds(x => x.FullName);
                c.ResolveConflictingActions(x => x.First());
                
                if (File.Exists($"../Documentation/{applicationConfig.ApiVersion}/XmlComments.xml"))
                {
                    c.IncludeXmlComments($"../Documentation/{applicationConfig.ApiVersion}/XmlComments.xml", true);
                    c.IncludeXmlComments($"../Documentation/{applicationConfig.ApiVersion}/XmlComments.Infrastructure.xml", true);
                    c.IncludeXmlComments($"../Documentation/{applicationConfig.ApiVersion}/XmlComments.Data.xml", true);
                }
                c.SwaggerGeneratorOptions.DocInclusionPredicate = (s, description) => description.ActionDescriptor.AttributeRouteInfo?.Name?.StartsWith("api_") ?? false;
                c.ParameterFilter<SwaggerCommonFilter>();
                c.DocumentFilter<SwaggerCommonFilter>();
                c.OperationFilter<SwaggerCommonFilter>();
            });

            return _appEngine.ConfigureServices(services, _hostingEnvironment, _configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
#if DEBUG || DEBUGWS
            app.UseSwagger();
#endif

            _appEngine.ConfigureApp(app, env);
        }
    }
}
