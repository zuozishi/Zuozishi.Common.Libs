using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class UserInfoResponse : ErrorResponse
    {
        /// <summary>
        /// 用户关注某个轻应用/订阅号后产生的唯一id
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 登录者名字
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// 对于已认证轻应用/订阅号，同unionid；对于企业内部轻应用/订阅号，同userid
        /// </summary>
        [JsonProperty("uid")]
        public string UID { get; set; }

        /// <summary>
        /// 同一个轻应用/订阅号在不同企业中的相同用户，拥有相同的unionid
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        /// <summary>
        /// 登录者的头像
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// 已认证轻应用在非公共轻应用模式下时，返回当前用户所在的企业id
        /// </summary>
        [JsonProperty("domainid")]
        public string DomainId { get; set; }

        /// <summary>
        /// 用户唯一id
        /// </summary>
        [JsonProperty("userid")]
        public string UserId { get; set; }

        /// <summary>
        /// 登录者角色，普通用户为0，轻应用管理员为1
        /// </summary>
        [JsonProperty("role")]
        public int Role { get; set; }

        /// <summary>
        /// 是否为企业管理员
        /// </summary>
        [JsonProperty("is_domain_manager")]
        public bool IsDomainManager { get; set; }

        /// <summary>
        /// 是否为轻应用/订阅号管理员
        /// </summary>
        [JsonProperty("is_app_manager")]
        public bool IsAppManager { get; set; }
    }
}
