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
using EvenCart.Infrastructure;
using EvenCart.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EvenCart
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvenCart Api Documentation", Version = ApplicationConfig.ApiVersion });
                c.CustomSchemaIds(x => x.FullName);
                c.ResolveConflictingActions(x => x.First());
                
                if (File.Exists($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.xml"))
                {
                    c.IncludeXmlComments($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.xml", true);
                    c.IncludeXmlComments($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.Infrastructure.xml", true);
                    c.IncludeXmlComments($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.Data.xml", true);
                }
                c.SwaggerGeneratorOptions.DocInclusionPredicate = (s, description) => description.ActionDescriptor.AttributeRouteInfo?.Name?.StartsWith("api_") ?? false;
                c.ParameterFilter<SwaggerCommonFilter>();
                c.DocumentFilter<SwaggerCommonFilter>();
                c.OperationFilter<SwaggerCommonFilter>();
            });

            return ApplicationEngine.ConfigureServices(services, _hostingEnvironment, _configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#if DEBUG || DEBUGWS
            app.UseSwagger();
#endif

            ApplicationEngine.Configure(app, env);
        }
    }
}
