namespace Microsoft.AspNetCore.Builder
{
    using Http;
    using System;
    using WebApiProxy.Core.Models;
    using WebApiProxy.Middleware;

    public static class WebApiProxyBuilderExtensions
        {
            public static IApplicationBuilder UseWebApiProxy(
                this IApplicationBuilder app,
                string routeTemplate = "")
            {
                return app.UseWebApiProxy(NullDocumentFilter, routeTemplate);
            }

            public static IApplicationBuilder UseWebApiProxy(
                this IApplicationBuilder app,
                Action<HttpRequest, Metadata> documentFilter,
                string routeTemplate = "")
            {
                return app.UseMiddleware<MetadataMiddleware>(documentFilter, routeTemplate);
            }

            private static void NullDocumentFilter(HttpRequest httpRequest, Metadata metadata)
            { }
        }
    }

