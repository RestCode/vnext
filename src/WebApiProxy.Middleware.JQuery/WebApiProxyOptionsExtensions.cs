using WebApiProxy.Providers.JQuery;

namespace WebApiProxy.Middleware
{
    public static class WebApiProxyOptionsExtensions
    {
        public static void AddJQueryClient(this WebApiProxyOptions options, string endpoint = "jquery", string action = "GET")
        {
            var clientOptions = new ClientProviderOptions
            {
                Endpoint = endpoint,
                HttpMethod = action
            };
            options.ClientProviderOptions.Add(typeof(JQueryClientProviderMiddleware), clientOptions);
        }
    }
}
