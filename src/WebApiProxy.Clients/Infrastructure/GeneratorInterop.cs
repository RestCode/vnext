namespace WebApiProxy.NodeJs.Infrastructure
{
    using Clients.Infrastructure;
    using Clients.Models;
    using Core.Infrastructure;
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;

    public class GeneratorInterop
    {
        public async Task<object> Process(object input)
        {
            var configuration = ((ExpandoObject)input).Map<ClientConfiguration>();
            var provider = new ExternalMetadataProvider(configuration);
            var generator = (IGenerator)Activator.CreateInstance(Type.GetType(configuration.Generator), provider, configuration);

            return await generator.Process();
        }
    }
}
