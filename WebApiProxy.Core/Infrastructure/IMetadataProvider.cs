using WebApiProxy.Core.Models;

namespace WebApiProxy.Core.Infrastructure
{
    public interface IMetadataProvider
    {
        Metadata GetMetadata();
    }
}
