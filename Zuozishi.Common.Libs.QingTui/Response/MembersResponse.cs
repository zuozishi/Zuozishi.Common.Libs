using Newtonsoft.Json;
using System.Collections.Generic;
using Zuozishi.Common.Libs.QingTui.Models;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class MembersResponse : ErrorResponse
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("result_list")]
        public List<QtMember> Members { get; set; }
    }
}
