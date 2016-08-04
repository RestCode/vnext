using System.Threading.Tasks;
using WebApiProxy.Core.Models;

namespace WebApiProxy.Core.Infrastructure
{
    public abstract class GeneratorBase : IGenerator
    {
        protected readonly ClientConfiguration configuration;
        public GeneratorBase()
        {

        }
        public GeneratorBase(IMetadataProvider metadataProvider, ClientConfiguration configuration)
        {
            this.Metadata = metadataProvider?.GetMetadata();
            this.configuration = configuration;
        }

        protected Metadata Metadata { get; private set; }
        public abstract Task<string> Process();
        
    }
}
