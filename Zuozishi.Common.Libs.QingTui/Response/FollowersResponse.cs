using System.Collections.Generic;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class FollowersResponse : ErrorResponse
    {
        /// <summary>
        /// 使用者的openid列表
        /// </summary>
        public List<string> Followers { get; set; }
        /// <summary>
        /// 是否还有更多的数据
        /// </summary>
        public bool HasMore { get; set; }
        /// <summary>
        /// 使用者总数量
        /// </summary>
        public long TotalCount { get; set; }
    }
}
