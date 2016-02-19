using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Graphics.Display;
using Windows.System.Profile;
using Windows.UI.Xaml.Controls;

namespace BaoVietCore.Helpers
{
    public enum DeviceTypes
    {
        Desktop,
        Mobile,
        Other
    }

    public enum AppState
    {
        Tablet,
        Mobile
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

        static double AppWidth = 0;
        public static double TableThreshold = 720;
        public static AppState GetAppState()
        {
            if (AppWidth >= TableThreshold)
                return AppState.Tablet;
            return AppState.Mobile;
        }

        public static void Configurate(Frame rootFrame, double tableThreshold)
        {
            TableThreshold = tableThreshold;
            rootFrame.SizeChanged += RootFrame_SizeChanged;
        }

        private static void RootFrame_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            AppWidth = e.NewSize.Width;
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
