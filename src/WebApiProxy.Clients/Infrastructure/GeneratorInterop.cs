namespace WebApiProxy.Clients
{
    using Infrastructure;
    using Clients.Models;
    using Core.Infrastructure;
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    public class GeneratorInterop
    {
        public async Task<object> Invoke(ExpandoObject input)
        {
            var configuration = input.Map<ClientConfiguration>();

            var assembly = Assembly.Load(new AssemblyName(configuration.GeneratorAssembly));
            var provider = new ExternalMetadataProvider(configuration);
            var metadata = provider.GetMetadata();
            var generator = (IGenerator)Activator.CreateInstance(assembly.GetType(configuration.GeneratorType), metadata);

            return await generator.Process();
        }
    }
}
