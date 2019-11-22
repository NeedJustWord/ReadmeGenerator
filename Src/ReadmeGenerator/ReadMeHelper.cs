using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ReadmeGenerator.Models;

namespace ReadmeGenerator
{
    class ReadMeHelper
    {
        private string root;
        private string readMeFilePath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root">根目录</param>
        /// <param name="readMeFilePath">readme文件目录</param>
        public ReadMeHelper(string root, string readMeFilePath)
        {
            this.root = root;
            this.readMeFilePath = readMeFilePath;
        }

        /// <summary>
        /// 生成readme.md文件
        /// </summary>
        public void CreateReadMeFie()
        {
            string filePath = Path.Combine(readMeFilePath, "readme.md");
            using (var stream = File.Create(filePath))
            {
                using (var sw = new StreamWriter(stream, Encoding.UTF8))
                {
                    var headerInfos = GetHeaderInfos();
                    foreach (var item in headerInfos)
                    {
                        sw.WriteLine(item);
                    }

                    var labelInfos = GetLabelInfos();
                    foreach (var labelInfo in labelInfos)
                    {
                        foreach (var item in labelInfo.GetWriteLines(readMeFilePath))
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取readme.md的头部信息
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetHeaderInfos()
        {
            var heading = ConfigHelper.GetAppSettingValue("Heading");
            yield return null;
            yield return MarkdownHelper.Heading1(heading);
            yield return null;
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        private IEnumerable<LabelInfo> GetLabelInfos()
        {
            var searchPattern = ConfigHelper.GetAppSettingValue("SearchPattern");
            var labelInfos = Directory.GetDirectories(root)
                .Select(path => new LabelInfo(path, Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly)));
            foreach (var item in labelInfos)
            {
                yield return item;
            }

            yield return new LabelInfo(Directory.GetFiles(root, searchPattern, SearchOption.TopDirectoryOnly));
        }
    }
}
