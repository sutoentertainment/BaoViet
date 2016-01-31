using BaoVietCore.Interfaces;
using LocalyticsComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Services
{
    public class LocalyticsAdapterService : ServiceBase, ITrackingService
    {
        private const string Key = "6c2c71b5a94492ba082a12c-7cb253fc-c7c4-11e5-a4a7-00342b7f5075";
        public LocalyticsAdapterService(Manager man) : base(man)
        {

        }

        public void AutoIntegrate()
        {
            Localytics.AutoIntegrate(Key);
        }

        public void TagEvent(string eventName)
        {
            Localytics.TagEvent(eventName);
        }

        public void TagEvent(string eventName, Dictionary<string, string> attribute)
        {
            Localytics.TagEvent(eventName, attribute);
        }

        public void TagScreen(string screenName)
        {
            Localytics.TagScreen(screenName);
        }
    }
}
