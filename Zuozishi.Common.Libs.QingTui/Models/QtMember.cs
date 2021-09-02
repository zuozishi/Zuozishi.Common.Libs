using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Models
{
    public class QtMember
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [JsonProperty("mail")]
        public string Mail { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 是否访客，yes是；no否
        /// </summary>
        [JsonProperty("guest")]
        public string Guest { get; set; }

        /// <summary>
        /// 企业内用户Id
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 所在的组织机构列表
        /// </summary>
        [JsonProperty("org_list")]
        public List<string> OrgList { get; set; }

        [JsonProperty("org_id")]
        public string OrgId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [JsonProperty("employee_id")]
        public string EmployeeId { get; set; }
    }
}
