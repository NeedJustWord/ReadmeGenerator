namespace ReadmeGenerator
{
    /// <summary>
    /// Markdown语法帮助类
    /// </summary>
    class MarkdownHelper
    {
        #region 超链接
        /// <summary>
        /// 行内式超链接
        /// </summary>
        /// <param name="linkText">链接文本</param>
        /// <param name="url">链接url</param>
        /// <param name="titleText">链接标题</param>
        /// <returns></returns>
        public static string LinkRowInner(string linkText, string url, string titleText = "")
        {
            if (titleText.IsNullOrEmpty())
            {
                return $"[{linkText}]({url})";
            }

            //titleText可以用''、""、()包着
            return $"[{linkText}]({url} '{titleText}')";
        }
        #endregion

        #region 标题
        /// <summary>
        /// 1号标题
        /// </summary>
        /// <param name="heading"></param>
        /// <returns></returns>
        public static string Heading1(string heading)
        {
            return $"# {heading}";
        }

        /// <summary>
        /// 2号标题
        /// </summary>
        /// <param name="heading"></param>
        /// <returns></returns>
        public static string Heading2(string heading)
        {
            return $"## {heading}";
        }

        /// <summary>
        /// 3号标题
        /// </summary>
        /// <param name="heading"></param>
        /// <returns></returns>
        public static string Heading3(string heading)
        {
            return $"### {heading}";
        }

        /// <summary>
        /// 4号标题
        /// </summary>
        /// <param name="heading"></param>
        /// <returns></returns>
        public static string Heading4(string heading)
        {
            return $"#### {heading}";
        }

        /// <summary>
        /// 5号标题
        /// </summary>
        /// <param name="heading"></param>
        /// <returns></returns>
        public static string Heading5(string heading)
        {
            return $"##### {heading}";
        }
        #endregion
    }
}
