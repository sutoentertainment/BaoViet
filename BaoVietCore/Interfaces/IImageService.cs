using System;
using System.Threading.Tasks;

namespace BaoVietCore.Interfaces
{
    public interface IImageService
    {
        event EventHandler<EventArgs> OnDownloadComplete;

        Task SaveImageToStorage(string url);
    }
}