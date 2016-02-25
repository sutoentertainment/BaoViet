using System.Threading.Tasks;

namespace BaoVietCore.Interfaces
{
    public interface ILogService
    {
        string LogText { get; set; }
        void Log(string text, params string[] textM);
        Task WriteLog(string filename = "log");
    }
}