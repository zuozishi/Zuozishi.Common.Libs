using Newtonsoft.Json;
using Zuozishi.Common.Libs.QingTui.Models;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class OrgsResponse : ErrorResponse
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("result_list")]
        public List<QtOrg> Orgs { get; set; }
    }
}
