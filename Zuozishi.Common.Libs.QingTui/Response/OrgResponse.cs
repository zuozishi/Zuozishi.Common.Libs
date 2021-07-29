using Newtonsoft.Json;
using Zuozishi.Common.Libs.QingTui.Models;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class OrgResponse : ErrorResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("grade")]
        public int Grade { get; set; }

        public QtOrg GetQtOrg()
        {
            return (QtOrg)MemberwiseClone();
        }
    }
}
