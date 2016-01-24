using Windows.Storage;

namespace ThHelper
{
    public class SettingHelper
    {
        public static object LoadSetting(string key)
        {
            if (ApplicationData.Current.LocalSettings.Values[key] != null)
            {
                return ApplicationData.Current.LocalSettings.Values[key];
            }
            else
            {
                return null;
            }
        }

        public static void SaveSetting(string key, object value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }
    }

}
