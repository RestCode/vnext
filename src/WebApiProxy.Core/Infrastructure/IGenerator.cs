using System.Threading.Tasks;

namespace WebApiProxy.Core.Infrastructure
{
    public interface IGenerator
    {
        Task<string> Process();
    }
}
