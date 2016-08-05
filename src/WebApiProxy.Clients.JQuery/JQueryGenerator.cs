namespace WebApiProxy.Generators.JQuery
{
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using Clients.Infrastructure;
    using Core.Infrastructure;
    using Core.Models;
    using Clients.Models;

    public partial class JQueryGenerator: GeneratorBase
    {
        public JQueryGenerator()
        {

        }
        
        public JQueryGenerator(Metadata metadata): base(metadata)
        {

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
