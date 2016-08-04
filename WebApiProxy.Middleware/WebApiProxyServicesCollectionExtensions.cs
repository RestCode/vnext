using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProxy.Core.Infrastructure;
using WebApiProxy.Server;

namespace Microsoft.Extensions.DependencyInjection
{
    public class WebApiProxyConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
            foreach (var controller in application.Controllers)
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }
        }
    }
    public static class SwaggerGenServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApiProxy(
            this IServiceCollection services
          //  Action<SwaggerGenOptions> setupAction = null
            )
        {
            services.Configure<MvcOptions>(c =>
                c.Conventions.Add(new WebApiProxyConvention()));

            //services.Configure(setupAction ?? (opts => { }));

            services.AddSingleton(CreateWebApiProxyProvider);

            return services;
        }

        public static void ConfigureWebApiProxy(
            this IServiceCollection services
           // Action<SwaggerGenOptions> setupAction
           )
        {
          //  services.Configure(setupAction);
        }

        private static IMetadataProvider CreateWebApiProxyProvider(IServiceProvider serviceProvider)
        {
            //var swaggerGenOptions = serviceProvider.GetRequiredService<IOptions<SwaggerGenOptions>>().Value;
            //var mvcJsonOptions = serviceProvider.GetRequiredService<IOptions<MvcJsonOptions>>().Value;
            var apiDescriptionsProvider = serviceProvider.GetRequiredService<IApiDescriptionGroupCollectionProvider>();

            //var schemaRegistryFactory = new SchemaRegistryFactory(
            //    mvcJsonOptions.SerializerSettings,
            //    swaggerGenOptions.GetSchemaRegistryOptions(serviceProvider)
            //);

            return new WebApiProxyProvider(
                apiDescriptionsProvider
            );
        }
    }
}
