using System.Configuration;

namespace ReadmeGenerator
{
    class ConfigHelper
    {
        public static string GetAppSettingValue(string key, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }
    }
}
