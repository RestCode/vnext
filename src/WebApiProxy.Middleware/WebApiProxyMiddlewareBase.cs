namespace WebApiProxy.Middleware
{
    using Core.Infrastructure;
    using Core.Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;
    public abstract class WebApiProxyMiddlewareBase
    {
        protected readonly RequestDelegate next;
        protected readonly IMetadataProvider metadataProvider;
        protected readonly WebApiProxyOptions options;
        protected Metadata metadata;

        public WebApiProxyMiddlewareBase(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            WebApiProxyOptions options
            )
        {
            this.next = next;
            this.metadataProvider = metadataProvider;
            this.options = options;
        }
        public abstract Task Invoke(HttpContext httpContext);

        protected virtual async Task ProcessRequest(HttpContext httpContext)
        {
            if (!ValidateRequest(httpContext.Request))
            {
                await next(httpContext);
                return;
            }

            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            metadata = metadataProvider.GetMetadata(host);
        }

        protected virtual bool ValidateRequest(HttpRequest request)
        {
            var isValid =
                (options != null &&
                request.Method.Equals(options.HttpMethod, StringComparison.OrdinalIgnoreCase) &&
                request.Path.Equals(options.AbsoluteEndpoint, StringComparison.OrdinalIgnoreCase));
            return isValid;
        }
    }
}
