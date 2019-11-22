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
        /// <param name="fullPath">链接全路径</param>
        public LinkInfo(string fullPath) : base(fullPath, Path.GetFileNameWithoutExtension(fullPath))
        {
        }

        public override IEnumerable<string> GetWriteLines(string readMeFilePath)
        {
            var url = FullPath.Replace(readMeFilePath, "").Replace('\\', '/').Replace(" ","%20").TrimStart('/');
            yield return MarkdownHelper.LinkRowInner(Name, url);
            yield return null;
        }
    }
}
