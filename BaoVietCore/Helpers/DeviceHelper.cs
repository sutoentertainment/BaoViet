using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Graphics.Display;
using Windows.System.Profile;

namespace BaoVietCore.Helpers
{
    public enum DeviceTypes
    {
        Desktop,
        Mobile,
        Other
    }

    public class DeviceHelper
    {
        public static DeviceTypes CurrentDevice()
        {
            string deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            switch (deviceFamily)
            {
                case "Windows.Desktop":
                    return DeviceTypes.Desktop;
                case "Windows.Mobile":
                    return DeviceTypes.Mobile;
                default:
                    return DeviceTypes.Other;

            }
        }

        public static string GetAppVersion()
        {
            PackageVersion pv = Package.Current.Id.Version;
            Version version = new Version(Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Revision,
                Package.Current.Id.Version.Build);
            string appVersion = version.Major + "." + version.Minor + "." + version.MinorRevision + "." + version.Build;
            return appVersion;
        }

        public static void LockDisplayOrientations(bool auto = true)
        {
            if (auto)
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait | DisplayOrientations.Landscape;
            else
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        }
    }
}
