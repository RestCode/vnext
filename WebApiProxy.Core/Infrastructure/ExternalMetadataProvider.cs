using System.Collections.Generic;
using WebApiProxy.Core.Models;

namespace WebApiProxy.Core.Infrastructure
{
    public class ExternalMetadataProvider : IMetadataProvider
    {
        private readonly ClientConfiguration configuration;

        public ExternalMetadataProvider(ClientConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Metadata GetMetadata()
        {
            //download metadata
            return new Metadata
            {
                Host = $"Special: {configuration.ProxyUrl}",
                Definitions = new List<ControllerDefinition>(),
                Models = new List<ModelDefinition>()
            };
        }
    }
}
