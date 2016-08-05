namespace Microsoft.Extensions.DependencyInjection
{
    using AspNetCore.Mvc;
    using AspNetCore.Mvc.ApiExplorer;
    using Options;
    using System;
    using WebApiProxy.Core.Infrastructure;
    using WebApiProxy.Middleware;

    public static class WebApiProxyServicesCollectionExtensions
    {
        public static IServiceCollection AddWebApiProxy(
            this IServiceCollection services,
            Action<WebApiProxyOptions> setupAction = null
            )
        {
            services.Configure<MvcOptions>(c =>
                c.Conventions.Add(new WebApiProxyConvention()));

            services.Configure(setupAction ?? (opts => { }));

            services.AddSingleton(createDefaultMetadataProvider);

            

            return services;
        }

        public static void ConfigureWebApiProxy(
            this IServiceCollection services,
            Action<WebApiProxyOptions> setupAction
           )
        {
            services.Configure(setupAction);
        }

        private static IMetadataProvider createDefaultMetadataProvider(IServiceProvider serviceProvider)
        {
            //var options = serviceProvider.GetRequiredService<IOptions<WebApiProxyOptions>>().Value;
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
