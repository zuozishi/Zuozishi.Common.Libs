namespace Zuozishi.Common.Libs.QingTui.Request
{
    /// <summary>
    /// 待办消息
    /// </summary>
    public class ProcessMessage
    {
        /// <summary>
        /// 发送消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息体
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 点击消息后跳转的链接
        /// </summary>
        public string Url { get; set; }

        public object ToMessageObject()
        {
            return new
            {
                title = Title,
                body = Body,
                url = Url
            };
        }
    }
}
