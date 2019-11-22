using System.Collections.Generic;

namespace ReadmeGenerator.Models
{
    /// <summary>
    /// 抽象类
    /// </summary>
    abstract class AbstractInfo
    {
        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath { get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        public AbstractInfo(string fullPath, string name)
        {
            FullPath = fullPath;
            Name = name;
        }

        /// <summary>
        /// 获取输出信息
        /// </summary>
        /// <param name="readMeFilePath"></param>
        /// <returns></returns>
        public abstract IEnumerable<string> GetWriteLines(string readMeFilePath);
    }
}
