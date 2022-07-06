namespace Zuozishi.Common.Libs.QingTui.Request
{
    /// <summary>
    /// 文本卡片消息
    /// </summary>
    public class TextCardMessage
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
        /// 按钮文本，默认为详情，最多6个字符
        /// </summary>
        public string ButtonText { get; set; } = "详情";

        /// <summary>
        /// 内容列表
        /// </summary>
        public IEnumerable<KeyValuePair<string, CardItemColor>> ContentList { get; set; }

        public object ToMessageObject()
        {
            var content_list = new List<object>();
            foreach (var item in ContentList)
            {
                content_list.Add(new
                {
                    text = item.Key,
                    attr = new
                    {
                        color = item.Value.ToString().ToUpper()
                    }
                });
            }
            return new
            {
                title = Title,
                url = Url,
                button_list = ButtonText,
                content_list = content_list
            };
        }
    }
}
