namespace WebApiProxy.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Routing.Template;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Core.Models;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Mvc;

    public class MetadataMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetadataProvider _metadataProvider;
        private readonly WebApiProxyOptions _options;
        //private readonly JsonSerializer _swaggerSerializer;
        //private readonly Action<HttpRequest, Metadata> _documentFilter;
        //private readonly TemplateMatcher _requestMatcher;

        
        public MetadataMiddleware(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            IOptions<WebApiProxyOptions> options
            //Action<HttpRequest, Metadata> documentFilter,
            //string routeTemplate
            )
        {
            _next = next;
            _metadataProvider = metadataProvider;
            _options = options.Value;
           // _swaggerSerializer = SwaggerSerializerFactory.Create(mvcJsonOptions);
           // _documentFilter = documentFilter;
          //  _requestMatcher = new TemplateMatcher(TemplateParser.Parse(routeTemplate), new RouteValueDictionary());
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string apiVersion;
            if (!isRequestingMetadata(httpContext.Request, out apiVersion))
            {
                await _next(httpContext);
                return;
            }

            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";


            var metadata = _metadataProvider.GetMetadata(host);

            // One last opportunity to modify the Swagger Document - this time with request context
           // _documentFilter(httpContext.Request, metadata);

            RespondWithMetadataJson(httpContext.Response, metadata);
            //await _next(httpContext);
        }

        private bool isRequestingMetadata(HttpRequest request, out string apiVersion)
        {
            apiVersion = null;
            if (request.Method.Equals(_options.HttpMethod,StringComparison.OrdinalIgnoreCase) && request.Path == _options.MetadataEndpoint)
            {
                return true;
            }

           // var routeValues = new RouteValueDictionary();
          //  if (!_requestMatcher.TryMatch(request.Path, routeValues) || !routeValues.ContainsKey("apiVersion")) return false;

         //   apiVersion = routeValues["apiVersion"].ToString();
            return false;
        }

        private void RespondWithMetadataJson(HttpResponse response, Metadata metadata)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json";
            

            using (var writer = new StreamWriter(response.Body))
            {

                var output = JsonConvert.SerializeObject(metadata);

                writer.Write(output);
               // _swaggerSerializer.Serialize(writer, metadata);
            }
        }
    }
}
