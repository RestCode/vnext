using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProxy.Core.Models;
using WebApiProxy.Server;

namespace Microsoft.AspNetCore.Builder
    {
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
                return app.UseMiddleware<WebApiProxyMiddleware>(documentFilter, routeTemplate);
            }

            private static void NullDocumentFilter(HttpRequest httpRequest, Metadata metadata)
            { }
        }
    }

