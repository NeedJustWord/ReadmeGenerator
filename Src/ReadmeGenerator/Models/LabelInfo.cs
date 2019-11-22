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
        /// <summary>
        /// 链接列表
        /// </summary>
        public List<LinkInfo> LinkInfos { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkFullPaths">链接全路径列表</param>
        public LabelInfo(string[] linkFullPaths) : base("未分类", "未分类")
        {
            LinkInfos = linkFullPaths.Select(path => new LinkInfo(path)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath">标签全路径</param>
        /// <param name="linkFullPaths">链接全路径列表</param>
        public LabelInfo(string fullPath, string[] linkFullPaths) : base(fullPath, Path.GetFileName(fullPath))
        {
            LinkInfos = linkFullPaths.Select(path => new LinkInfo(path)).ToList();
        }

        public override IEnumerable<string> GetWriteLines(string readMeFilePath)
        {
            if (LinkInfos.Count > 0)
            {
                yield return MarkdownHelper.Heading2(Name);
                yield return null;

                foreach (var linkInfo in LinkInfos)
                {
                    foreach (var item in linkInfo.GetWriteLines(readMeFilePath))
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
