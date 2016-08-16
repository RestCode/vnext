namespace WebApiProxy.Generators.JQuery
{
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Core.Models;

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
