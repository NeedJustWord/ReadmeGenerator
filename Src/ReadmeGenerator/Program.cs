using System;
using System.IO;
using System.Reflection;
using ReadmeGenerator.Models;

namespace ReadmeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
            var version = $"v{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";
            Console.Title = $"Readme生成器 {version}";

            var blogRoot = GetFullPath(ConfigInfo.Root);
            var readMeFilePath = GetFullPath(ConfigInfo.ReadMeFilePath);
            var readMe = new ReadMeHelper();
            readMe.CreateReadMeFie(blogRoot, readMeFilePath);
        }

        static string GetFullPath(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }
            return Path.GetFullPath(path);
        }
    }
}
