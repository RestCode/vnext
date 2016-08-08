namespace WebApiProxy.Clients
{
    using Infrastructure;
    using Clients.Models;
    using Core.Infrastructure;
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class GeneratorInterop
    {
        public async Task<object> Invoke(object input)
        {
            IDictionary<string, object> parameters = (ExpandoObject)input;
            var configuration = ((ExpandoObject)input).Map<ClientConfiguration>();

            //var configuration = new ClientConfiguration
            //{
            //    Generator = parameters["generator"]?.ToString(),
            //    MetadataEndpoint = parameters["metadataEndpoint"]?.ToString(),
            //    Method = parameters["method"]?.ToString(),
            //    Name = parameters["name"]?.ToString(),
            //    Namespace = parameters["namespace"]?.ToString(),
            //    Suffix = parameters["suffix"]?.ToString(),
            //};


            var provider = new ExternalMetadataProvider(configuration);
            var metadata = provider.GetMetadata();
            var generator = (IGenerator)Activator.CreateInstance(Type.GetType(configuration.Generator), metadata);

            return await generator.Process();
        }
    }
}
