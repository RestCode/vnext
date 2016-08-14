namespace WebApiProxy.Generators
{
    using Infrastructure;
    using Core.Infrastructure;
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using System.Reflection;
    using Models;

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
