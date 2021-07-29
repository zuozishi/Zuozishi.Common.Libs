using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Models
{
    public class QtOrg
    {
        /// <summary>
        /// 组织机构id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 父组织机构id
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// 组织机构显示顺序
        /// </summary>
        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        /// <summary>
        /// 组织机构等级名称
        /// </summary>
        [JsonProperty("grade")]
        public int Grade { get; set; }
    }
}
