namespace Microsoft.AspNetCore.Builder
{
    using WebApiProxy.Middleware;

    public static class WebApiProxyBuilderExtensions
    {
        public static IApplicationBuilder UseWebApiProxy(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<WebApiProxyMetadataProviderMiddleware>();
        }
    }
}

