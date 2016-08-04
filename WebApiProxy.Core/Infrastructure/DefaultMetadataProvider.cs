using WebApiProxy.Core.Models;

namespace WebApiProxy.Core.Infrastructure
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        private readonly Metadata metadata;

        public DefaultMetadataProvider(Metadata metadata)
        {
            this.metadata = metadata;
        }
        public Metadata GetMetadata()
        {
            return metadata;
        }
    }
}
