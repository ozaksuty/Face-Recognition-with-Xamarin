using FaceSample.Services;
using System.IO;
using System.Threading.Tasks;

namespace FaceSample.Provider
{
    public interface IServiceManager : IBaseService
    {
        Task<T> Get<T>(string url);
        Task<T> Post<T, K>(K requestModel, string url);
        Task<T> PostStream<T>(Stream stream, string url);
    }
}