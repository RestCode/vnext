namespace Microsoft.AspNetCore.Builder
{
    using WebApiProxy.Middleware.JQuery;

    public static class JQueryProviderBuilderExtensions
    {
        public static IApplicationBuilder UseJQueryClientProvider(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<JQueryClientProviderMiddleware>();
        }
        
    }
}
