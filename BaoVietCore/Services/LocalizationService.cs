using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace BaoVietCore.Services
{
    public class LocalizationService : ServiceBase, ILocalizationService
    {
        ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();

        public LocalizationService(Manager manager) : base(manager)
        {

        }
        
        public string GetString(string name)
        {
            return resourceLoader.GetString(name);
        }
    }
}
