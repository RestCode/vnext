namespace WebApiProxy.Generators.JQuery
{
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using Clients.Infrastructure;
    using Core.Infrastructure;
    using Core.Models;

    public partial class JQueryGenerator: GeneratorBase
    {
        public JQueryGenerator()
        {

        }
        
        public JQueryGenerator(Metadata metadata, ClientConfiguration configuration): base(metadata, configuration)
        {

        }

        public override async Task<string> Process()
        {
            return await Task.Run(() =>
            {
                return this.transformText();
            });

        }

        public async Task<object> ProcessFromProxy(object input)
        {
            var configuration = ((ExpandoObject)input).Map<ClientConfiguration>();
            var provider = new ExternalMetadataProvider(configuration);
            var generator = (IGenerator)Activator.CreateInstance(Type.GetType(configuration.Generator), provider, configuration);

            return await generator.Process();
        }


    }
}
