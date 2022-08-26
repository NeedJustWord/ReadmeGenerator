using System;
using System.Configuration;
using System.IO;

namespace ReadmeGenerator.Models
{
    static class ConfigInfo
    {
        public static readonly string Heading;
        public static readonly string SearchPattern;
        public static readonly string Root;
        public static readonly string ReadMeFilePath;
        public static readonly bool IsPrintCatalogue;
        public static readonly bool IsPrintExtension;
        public static readonly bool IsPrintOrder;
        public static readonly Func<string, string> GetNameFunc;

        static ConfigInfo()
        {
            Heading = GetAppSettingValue("Heading");
            SearchPattern = GetAppSettingValue("SearchPattern");
            Root = GetAppSettingValue("Root");
            ReadMeFilePath = GetAppSettingValue("ReadMeFilePath");
            IsPrintCatalogue = GetAppSettingValue("IsPrintCatalogue") == "1";
            IsPrintExtension = GetAppSettingValue("IsPrintExtension") == "1";
            IsPrintOrder = GetAppSettingValue("IsPrintOrder") == "1";
            GetNameFunc = IsPrintExtension ? (Func<string, string>)Path.GetFileName : Path.GetFileNameWithoutExtension;
        }

        private static string GetAppSettingValue(string key, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }
    }
}
