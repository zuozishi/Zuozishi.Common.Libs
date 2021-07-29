using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class DomainIdResponse : ErrorResponse
    {
        [JsonProperty("domainId")]
        public string DomainId { get; set; }
    }
}
