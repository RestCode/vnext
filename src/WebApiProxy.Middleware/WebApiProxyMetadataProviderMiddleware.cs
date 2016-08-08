namespace WebApiProxy.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Microsoft.Extensions.Options;
    public class WebApiProxyMetadataProviderMiddleware : WebApiProxyMiddlewareBase
    {
        public WebApiProxyMetadataProviderMiddleware(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            IOptions<WebApiProxyProviderOptions> options
            ): base(next, metadataProvider, options.Value) { }
        public async override Task Invoke(HttpContext httpContext)
        {

            await base.ProcessRequest(httpContext);
            
            var response = httpContext.Response;
            response.StatusCode = 200;
            response.ContentType = "application/json";
            using (var writer = new StreamWriter(response.Body))
            {
                var output = JsonConvert.SerializeObject(metadata);

                writer.Write(output);
            }
        }
    }
}
