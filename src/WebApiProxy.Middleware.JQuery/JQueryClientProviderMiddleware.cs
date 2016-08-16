namespace WebApiProxy.Middleware.JQuery
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Clients.JQuery;
    using Microsoft.Extensions.Options;
    using Middleware;

    public class JQueryClientProviderMiddleware: WebApiProxyMiddlewareBase
    {
        public JQueryClientProviderMiddleware(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            IOptions<JQueryClientProviderOptions> options):base(next,metadataProvider,options.Value)
        {
        }

        public async override Task Invoke(HttpContext httpContext)
        {
            if (await base.ProcessRequest(httpContext))
            {
                IGenerator generator = new JQueryGenerator(metadata);
                var result = await generator.Process();
                await httpContext.Response.WriteAsync(result);
            }

            

        }
        

    }
}
