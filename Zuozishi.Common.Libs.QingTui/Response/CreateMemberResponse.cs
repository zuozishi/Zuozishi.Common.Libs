using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class CreateMemberResponse : ErrorResponse
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
