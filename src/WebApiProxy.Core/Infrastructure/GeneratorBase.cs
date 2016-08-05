namespace WebApiProxy.Core.Infrastructure
{
    using System.Threading.Tasks;
    using Models;
    public abstract class GeneratorBase : IGenerator
    {
        //protected readonly ClientConfiguration configuration;
        public GeneratorBase()
        {

        }
        public GeneratorBase(Metadata metadata)
        {
            this.Metadata = metadata;
            //this.configuration = configuration;
        }

        protected Metadata Metadata { get; private set; }
        public abstract Task<string> Process();
        
    }
}
