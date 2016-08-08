namespace WebApiProxy.Middleware
{
    using Core.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    public static class WebApiProxySetup
    {
        public static void ConfigureDefaultServices<T>(IServiceCollection services, Action<T> setupAction = null) where T : WebApiProxyOptions
        {
            services.Configure<MvcOptions>(c =>
                c.Conventions.Add(new WebApiProxyConvention()));

            services.Configure(setupAction ?? (opts => { }));

            services.AddSingleton(createDefaultMetadataProvider);

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
