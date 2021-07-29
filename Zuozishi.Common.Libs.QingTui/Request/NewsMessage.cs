namespace Zuozishi.Common.Libs.QingTui.Request
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsMessage
    {
        /// <summary>
        /// 标题，最多45个字符
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 点击后的链接去向地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 摘要，最多120个字符
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片media_id
        /// </summary>
        public string ThumbMediaId { get; set; }

        public object ToMessageObject() => new
        {
            title = Title,
            url = Url,
            content = Content,
            thumbMediaId = ThumbMediaId,
        };
    }
}
