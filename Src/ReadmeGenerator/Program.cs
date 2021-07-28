using System;
using System.IO;
using System.Reflection;

namespace ReadmeGenerator
{
    class Program
    {
        private static string RootConfig = "Root";
        private static string ReadMeFilePathConfig = "ReadMeFilePath";

        static void Main(string[] args)
        {
            var assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
            var version = $"v{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";
            Console.Title = $"Readme生成器 {version}";

            if (!InitPath(RootConfig, "内容根目录", out string blogRoot))
            {
                return;
            }

            if (!InitPath(ReadMeFilePathConfig, "readme.md文件所在目录", out string readMeFilePath))
            {
                return;
            }

            ReadMeHelper readMe = new ReadMeHelper(blogRoot, readMeFilePath);
            readMe.CreateReadMeFie();

            WriteMessage("生成readme.md文件成功");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pathName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static bool InitPath(string key, string pathName, out string path)
        {
            path = GetAppSettingValue(key);
            if (path.IsNullOrWhiteSpace())
            {
                WriteMessageAndWaitKey($"获取{pathName}失败");
                return false;
            }

            if (!Directory.Exists(path))
            {
                WriteMessageAndWaitKey($"{pathName}不存在，请检查配置文件");
                return false;
            }

            path = Path.GetFullPath(path);
            return true;
        }

        /// <summary>
        /// 获取指定key配置的路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static string GetAppSettingValue(string key)
        {
            var root = ConfigHelper.GetAppSettingValue(key);
            if (root.IsNullOrWhiteSpace())
            {
                return "";
            }

            if (!Path.IsPathRooted(root))
            {
                root = Path.Combine(Environment.CurrentDirectory, root);
            }

            return root;
        }

        /// <summary>
        /// 输出消息并等待用户按下任意键
        /// </summary>
        /// <param name="message"></param>
        static void WriteMessageAndWaitKey(string message)
        {
            Console.WriteLine($"{message}，按任意键退出！");
            Console.ReadKey();
        }

        static void WriteMessage(string message)
        {
            Console.WriteLine($"{message}，按任意键退出！");
        }
    }
}
