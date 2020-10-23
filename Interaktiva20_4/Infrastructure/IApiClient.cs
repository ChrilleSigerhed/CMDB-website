using System.Threading.Tasks;

namespace Interaktiva20_4.Infrastructure
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string endpoint);
    }
}
