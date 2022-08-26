using System.Collections.Generic;

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
        public LinkInfo(string readMeFilePath, string fullPath, int level, string order)
            : base(readMeFilePath, fullPath, ConfigInfo.GetNameFunc(fullPath), level, order)
        {
        }

        public override IEnumerable<string> GetWriteLines()
        {
            yield return $"{GetWhiteSpace()}{MarkdownHelper.LinkRowInner(LinkText, Url)}";
            yield return null;
        }
    }
}
