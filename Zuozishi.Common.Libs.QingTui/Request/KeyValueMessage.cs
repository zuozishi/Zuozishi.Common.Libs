using System.Collections.Generic;

namespace Zuozishi.Common.Libs.QingTui.Request
{
    /// <summary>
    /// key-value消息
    /// </summary>
    public class KeyValueMessage
    {
        /// <summary>
        /// 标题，最多45个字符
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 首行说明
        /// </summary>
        public KeyValuePair<string, CardItemColor>? SubTitle { get; set; }
        /// <summary>
        /// 点击后的链接去向地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 正文，最长支持6个键值对
        /// </summary>
        public IEnumerable<KeyValueMessageItem> Content { get; set; }
        /// <summary>
        /// 末尾说明
        /// </summary>
        public KeyValuePair<string, CardItemColor>? FooterText { get; set; }
        /// <summary>
        /// 按钮文本，默认为详情，最多6个字符
        /// </summary>
        public string ButtonText { get; set; } = "详情";

        public object ToMessageObject()
        {
            object sub_title = null;
            object footer = null;
            var content = new List<object>();
            if (SubTitle != null)
                sub_title = new
                {
                    text = SubTitle.Value.Key,
                    color = SubTitle.Value.Value.ToString().ToUpper()
                };
            if (FooterText != null)
                footer = new
                {
                    text = FooterText.Value.Key,
                    color = FooterText.Value.Value.ToString().ToUpper()
                };
            foreach (var item in Content)
            {
                content.Add(new
                {
                    key = item.Key,
                    value = item.Value,
                    valueColor = item.ValueColor.ToString().ToUpper()
                });
            }
            return new
            {
                title = Title,
                sub_title = sub_title,
                url = Url,
                content = content,
                footer = footer,
                button_text = ButtonText
            };
        }
    }

    public class KeyValueMessageItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public CardItemColor ValueColor { get; set; }
    }
}
