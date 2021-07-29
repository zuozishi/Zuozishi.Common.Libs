using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class OrgIdResponse : ErrorResponse
    {
        [JsonProperty("org_id")]
        public string OrgId { get; set; }
    }
}
