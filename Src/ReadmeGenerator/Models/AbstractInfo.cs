using System.Collections.Generic;
using System.Text;

namespace ReadmeGenerator.Models
{
    /// <summary>
    /// 抽象类
    /// </summary>
    abstract class AbstractInfo
    {
        /// <summary>
        /// readme文件目录
        /// </summary>
        protected readonly string ReadMeFilePath;
        /// <summary>
        /// 全路径
        /// </summary>
        protected readonly string FullPath;
        /// <summary>
        /// 名称
        /// </summary>
        protected readonly string Name;
        /// <summary>
        /// 层级
        /// </summary>
        protected readonly int Level;
        /// <summary>
        /// 序号
        /// </summary>
        protected readonly int Order;

        protected AbstractInfo(string readMeFilePath, string fullPath, string name, int level, int order)
        {
            ReadMeFilePath = readMeFilePath;
            FullPath = fullPath;
            Name = name;
            Level = level;
            Order = order;
        }

        /// <summary>
        /// 获取输出信息
        /// </summary>
        /// <param name="readMeFilePath"></param>
        /// <returns></returns>
        public abstract IEnumerable<string> GetWriteLines();

        protected string GetWhiteSpace()
        {
            string whiteSpace = "&emsp;&emsp;";
            StringBuilder sb = new StringBuilder(whiteSpace.Length * Level);
            for (int i = 0; i < Level; i++)
            {
                sb.Append(whiteSpace);
            }
            return sb.ToString();
        }

        protected string GetUrl()
        {
            return FullPath.Replace(ReadMeFilePath, "").Replace('\\', '/').Replace(" ", "%20").TrimStart('/');
        }
    }
}
