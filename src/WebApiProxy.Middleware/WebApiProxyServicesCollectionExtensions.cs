namespace Microsoft.Extensions.DependencyInjection
{
    using AspNetCore.Mvc;
    using AspNetCore.Mvc.ApiExplorer;
    using AspNetCore.Mvc.ApplicationModels;
    using System;
    using WebApiProxy.Core.Infrastructure;
    using WebApiProxy.Middleware;

    public static class WebApiProxyServicesCollectionExtensions
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

            return new DefaultMetadataProvider(apiDescriptionsProvider);

           
        }
    }
}
