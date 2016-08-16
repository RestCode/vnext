namespace WebApiProxy.Clients.CSharp
{
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Core.Models;

  
    public class Configuration
    {
        public const string ConfigFileName = "WebApiProxy.config";
        public const string CacheFile = "WebApiProxy.generated.cache";

        private string _clientSuffix = "Client";
        private string _name = "MyWebApiProxy";
        private bool _generateOnBuild = false;
        private string _namespace = "WebApi.Proxies";
        private bool _generateAsyncReturnTypes = false;

       
        public bool GenerateOnBuild
        {
            get
            {
                return this._generateOnBuild;
            }
            set
            {
                this._generateOnBuild = value;
            }
        }
        
        public string ClientSuffix
        {
            get
            {
                return _clientSuffix.DefaultIfEmpty("Client");
            }
            set
            {
                _clientSuffix = value;
            }
        }

        
        public string Namespace
        {
            get
            {
                return this._namespace;
            }
            set
            {
                this._namespace = value;
            }
        }
        
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        
        public string Endpoint { get; set; }
        
        public bool GenerateAsyncReturnTypes
        {
            get
            {
                return _generateAsyncReturnTypes;
            }
            set
            {
                _generateAsyncReturnTypes = value;
            }
        }


        public Metadata Metadata { get; set; }

        

    }
    public partial class CSharpGenerator : GeneratorBase
    {
        public Configuration Configuration { get; set; }
        public CSharpGenerator()
        {

        }
        public CSharpGenerator(Metadata metadata) : base(metadata)
        {
            this.Configuration = new Configuration();
            this.Configuration.Metadata = metadata;
        }
        public CSharpGenerator(Configuration configuration ) : base(null)
        {
            this.Configuration = configuration;
        }

        public override async Task<string> Process()
        {
            return await Task.Run(() =>
            {
                return this.transformText();
            });

        }




    }
}
