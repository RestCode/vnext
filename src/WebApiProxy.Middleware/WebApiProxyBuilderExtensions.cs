namespace Microsoft.AspNetCore.Builder
{
    using Http;
    using Microsoft.Extensions.Options;
    using System;
    using WebApiProxy.Core.Models;
    using WebApiProxy.Middleware;

    public static class WebApiProxyBuilderExtensions
        {
         

            public static IApplicationBuilder UseWebApiProxy(
                this IApplicationBuilder app)
            {
            


            return app.UseMiddleware<MetadataMiddleware>();
            }

            private static void NullDocumentFilter(HttpRequest httpRequest, Metadata metadata)
            { }
        }
    }

