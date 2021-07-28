using System.Collections.Generic;
using System.IO;

namespace ReadmeGenerator.Models
{
    /// <summary>
    /// 链接信息
    /// </summary>
    class LinkInfo : AbstractInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="readMeFilePath">readme文件目录</param>
        /// <param name="fullPath">全路径</param>
        /// <param name="level">层级</param>
        /// <param name="order">序号</param>
        public LinkInfo(string readMeFilePath, string fullPath, int level, int order)
            : base(readMeFilePath, fullPath, Path.GetFileName(fullPath), level, order)
        {
        }

        public override IEnumerable<string> GetWriteLines()
        {
            var url = GetUrl();
            var linkText = $"{Order}.{Name}";
            yield return $"{GetWhiteSpace()}{MarkdownHelper.LinkRowInner(linkText, url)}";
            yield return null;
        }
    }
}
