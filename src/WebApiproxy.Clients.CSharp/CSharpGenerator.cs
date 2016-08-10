namespace WebApiProxy.Clients.CSharp
{
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Core.Models;

    public partial class CSharpGenerator : GeneratorBase
    {
        public CSharpGenerator()
        {

        }

        public CSharpGenerator(Metadata metadata) : base(metadata)
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
