using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ReadmeGenerator.Models;

namespace ReadmeGenerator
{
    class ReadMeHelper
    {
        /// <summary>
        /// 生成readme.md文件
        /// </summary>
        /// <param name="root">根目录</param>
        /// <param name="readMeFilePath">readme文件目录</param>
        public void CreateReadMeFie(string root, string readMeFilePath)
        {
            string filePath = Path.Combine(readMeFilePath, "readme.md");
            using (var stream = File.Create(filePath))
            {
                using (var sw = new StreamWriter(stream, Encoding.UTF8))
                {
                    foreach (var item in GetHeaderInfos())
                    {
                        sw.WriteLine(item);
                    }

                    var rootInfo = new LabelInfo(readMeFilePath, root, -1, "", ConfigInfo.SearchPattern);
                    if (ConfigInfo.IsPrintCatalogue)
                    {
                        foreach (var item in GetCatalogueInfos(rootInfo))
                        {
                            sw.WriteLine(item);
                        }
                    }

                    foreach (var item in GetContentInfos(rootInfo))
                    {
                        sw.WriteLine(item);
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
            yield return MarkdownHelper.Heading1(ConfigInfo.Heading);
            yield return null;
        }

        /// <summary>
        /// 获取readme.md的目录信息
        /// </summary>
        /// <param name="rootInfo"></param>
        /// <returns></returns>
        private IEnumerable<string> GetCatalogueInfos(LabelInfo rootInfo)
        {
            yield return MarkdownHelper.Heading3("目录");
            yield return null;
            foreach (var item in rootInfo.LabelInfos.SelectMany(t => t.GetCatalogueLines()))
            {
                yield return item;
            }
            yield return null;
            yield return null;
            yield return MarkdownHelper.Heading3("内容");
            yield return null;
        }

        /// <summary>
        /// 获取readme.md的内容信息
        /// </summary>
        /// <param name="rootInfo"></param>
        /// <returns></returns>
        private IEnumerable<string> GetContentInfos(LabelInfo rootInfo)
        {
            foreach (var item in rootInfo.Infos.SelectMany(t => t.GetWriteLines()))
            {
                yield return item;
            }
        }
    }
}
