using System;
using System.Linq;
using EvenCart.Infrastructure;
using EvenCart.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EvenCart
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EvenCart Api Documentation", Version = ApplicationConfig.ApiVersion });
                c.CustomSchemaIds(x => x.FullName);
                c.ResolveConflictingActions(x => x.First());
                c.IncludeXmlComments($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.xml", true);
                c.IncludeXmlComments($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.Infrastructure.xml", true);
                c.IncludeXmlComments($"../Documentation/{ApplicationConfig.ApiVersion}/XmlComments.Data.xml", true);
                c.SwaggerGeneratorOptions.DocInclusionPredicate = (s, description) => description.ActionDescriptor.AttributeRouteInfo?.Name?.StartsWith("api_") ?? false;
                c.ParameterFilter<SwaggerCommonFilter>();
                c.DocumentFilter<SwaggerCommonFilter>();
                c.OperationFilter<SwaggerCommonFilter>();
                c.SchemaFilter<SwaggerCommonFilter>();
            });

            return ApplicationEngine.ConfigureServices(services, _hostingEnvironment);
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
