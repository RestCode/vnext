namespace WebApiProxy.Core.Infrastructure
{
    using Models;
    public interface IMetadataProvider
    {
        Metadata GetMetadata();
    }
}
