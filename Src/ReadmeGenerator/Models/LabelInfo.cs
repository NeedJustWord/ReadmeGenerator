using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadmeGenerator.Models
{
    /// <summary>
    /// 标签信息
    /// </summary>
    class LabelInfo : AbstractInfo
    {
        public readonly List<LabelInfo> LabelInfos;
        public readonly List<LinkInfo> LinkInfos;
        public readonly List<AbstractInfo> Infos;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readMeFilePath">readme文件目录</param>
        /// <param name="fullPath">全路径</param>
        /// <param name="level">层级</param>
        /// <param name="order">序号</param>
        /// <param name="searchPattern">文件搜索字符串</param>
        public LabelInfo(string readMeFilePath, string fullPath, int level, string order, string searchPattern)
            : base(readMeFilePath, fullPath, ConfigInfo.GetNameFunc(fullPath), level, order)
        {
            int tempOrder = 1;
            LabelInfos = Directory.GetDirectories(fullPath)
                .Where(path =>
                {
                    var name = Path.GetFileName(path).ToLower();
                    return ConfigInfo.SkipDirs.All(t => t != name);
                })
                .Select((path, i) => new LabelInfo(readMeFilePath, path, level + 1, $"{order}{tempOrder + i}.", searchPattern))
                .ToList();
            LinkInfos = Directory.GetFiles(fullPath, searchPattern)
                .Select((path, i) => new LinkInfo(readMeFilePath, path, level + 1, $"{order}{LabelInfos.Count + tempOrder + i}."))
                .ToList();
            Infos = LabelInfos.Cast<AbstractInfo>().Concat(LinkInfos).ToList();
        }

        public override IEnumerable<string> GetWriteLines()
        {
            yield return MarkdownHelper.Heading4($"{GetWhiteSpace()}{MarkdownHelper.LinkRowInner(LinkText, Url)}");
            yield return null;

            foreach (var item in Infos.SelectMany(t => t.GetWriteLines()))
            {
                yield return item;
            }
        }

        public IEnumerable<string> GetCatalogueLines()
        {
            var pointName = $"#{LinkText.Replace(" ", "-").Replace(".", "").Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "").ToLower()}-1";
            yield return MarkdownHelper.Heading4($"{GetWhiteSpace()}{MarkdownHelper.LinkRowInner(LinkText, pointName)}");
            yield return null;

            foreach (var item in LabelInfos.SelectMany(t => t.GetCatalogueLines()))
            {
                yield return item;
            }
        }
    }
}
