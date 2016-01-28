using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace BaoVietCore.Interfaces
{
    public interface IWebService
    {
        void CancelCurrentRequests();
        Task<string> GetString(string url);
        Task<Stream> MakeRawGetRequest(string url);
    }
}