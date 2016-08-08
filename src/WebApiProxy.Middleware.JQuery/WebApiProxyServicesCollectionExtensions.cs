namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using WebApiProxy.Middleware;
    using WebApiProxy.Providers.JQuery;
    
    public static class WebApiProxyServicesCollectionExtensions
    {
        public static IServiceCollection AddJQueryClientProvider(
            this IServiceCollection services,
            Action<JQueryClientProviderOptions> setupAction = null
            )
        {
            WebApiProxySetup.ConfigureDefaultServices(services, setupAction);
            
            return services;
        }
    }
}
