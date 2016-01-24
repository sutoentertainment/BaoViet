using System.Threading.Tasks;

namespace BaoVietCore.Interfaces
{
    public interface IWebService
    {
        void CancelCurrentRequests();
        Task<string> GetString(string url);
    }
}