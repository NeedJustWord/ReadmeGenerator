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
        private readonly List<LabelInfo> labelInfos;
        private readonly List<LinkInfo> linkInfos;

        public IEnumerable<AbstractInfo> Infos => labelInfos.Cast<AbstractInfo>().Concat(linkInfos);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readMeFilePath">readme文件目录</param>
        /// <param name="fullPath">全路径</param>
        /// <param name="level">层级</param>
        /// <param name="order">序号</param>
        /// <param name="searchPattern">文件搜索字符串</param>
        public LabelInfo(string readMeFilePath, string fullPath, int level, int order, string searchPattern)
            : this(readMeFilePath, fullPath, Path.GetFileName(fullPath), level, order, searchPattern)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readMeFilePath">readme文件目录</param>
        /// <param name="fullPath">全路径</param>
        /// <param name="name">名称</param>
        /// <param name="level">层级</param>
        /// <param name="order">序号</param>
        /// <param name="searchPattern">文件搜索字符串</param>
        private LabelInfo(string readMeFilePath, string fullPath, string name, int level, int order, string searchPattern)
            : base(readMeFilePath, fullPath, name, level, order)
        {
            int tempOrder = 1;
            labelInfos = Directory.GetDirectories(fullPath)
                .Select((path, i) => new LabelInfo(readMeFilePath, path, level + 1, tempOrder + i, searchPattern))
                .ToList();
            linkInfos = Directory.GetFiles(fullPath, searchPattern)
                .Select((path, i) => new LinkInfo(readMeFilePath, path, level + 1, labelInfos.Count + tempOrder + i))
                .ToList();
        }

        public override IEnumerable<string> GetWriteLines()
        {
            var url = GetUrl();
            var linkText = $"{Order}.{Name}";
            yield return $"{GetWhiteSpace()}{MarkdownHelper.LinkRowInner(linkText, url)}";
            yield return null;

            foreach (var item in Infos.SelectMany(t => t.GetWriteLines()))
            {
                yield return item;
            }
        }
    }
}
