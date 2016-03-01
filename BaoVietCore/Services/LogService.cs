using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BaoVietCore.Services
{
    public class LogService : ServiceBase, ILogService
    {
        public string LogText { get; set; } = "";
        StorageService storageService;

        public LogService(Manager man, StorageService _storageService) : base(man)
        {
            storageService = _storageService;
        }

        public void Log(string text, params string[] textM)
        {
            if (string.IsNullOrEmpty(text))
                return;
            LogText += DateTime.Now.ToString() + ": " + text + "\r\n";
            foreach (var item in textM)
            {
                if (!string.IsNullOrEmpty(text))
                    LogText += DateTime.Now.ToString() + ": " + item + "\r\n";
            }
        }

        public async Task WriteLog(string filename = "log.txt")
        {
            var logfile = await storageService.CreateFile(ApplicationData.Current.TemporaryFolder, filename, CreationCollisionOption.GenerateUniqueName);
            await storageService.WriteStringToFile(logfile, LogText);
        }
    }
}
