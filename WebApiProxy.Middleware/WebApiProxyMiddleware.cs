using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiProxy.Core.Infrastructure;
using WebApiProxy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProxy.Server
{
    public class WebApiProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetadataProvider _metadataProvider;
        //private readonly JsonSerializer _swaggerSerializer;
        private readonly Action<HttpRequest, Metadata> _documentFilter;
        private readonly TemplateMatcher _requestMatcher;

        public WebApiProxyMiddleware(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            //IOptions<MvcJsonOptions> mvcJsonOptions,
            Action<HttpRequest, Metadata> documentFilter,
            string routeTemplate)
        {
            _next = next;
            _metadataProvider = metadataProvider;
           // _swaggerSerializer = SwaggerSerializerFactory.Create(mvcJsonOptions);
            _documentFilter = documentFilter;
            _requestMatcher = new TemplateMatcher(TemplateParser.Parse(routeTemplate), new RouteValueDictionary());
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string apiVersion;
            if (!RequestingSwaggerDocs(httpContext.Request, out apiVersion))
            {
                await _next(httpContext);
                return;
            }

            var basePath = string.IsNullOrEmpty(httpContext.Request.PathBase)
                ? "/"
                : httpContext.Request.PathBase.ToString();

            var swagger = _metadataProvider.GetMetadata();

            // One last opportunity to modify the Swagger Document - this time with request context
            _documentFilter(httpContext.Request, swagger);

            RespondWithSwaggerJson(httpContext.Response, swagger);
            await _next(httpContext);
        }

        private bool RequestingSwaggerDocs(HttpRequest request, out string apiVersion)
        {
            apiVersion = null;
            if (request.Method != "OPTIONS") return false;

           // var routeValues = new RouteValueDictionary();
          //  if (!_requestMatcher.TryMatch(request.Path, routeValues) || !routeValues.ContainsKey("apiVersion")) return false;

         //   apiVersion = routeValues["apiVersion"].ToString();
            return true;
        }

        private void RespondWithSwaggerJson(HttpResponse response, Metadata metadata)
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
