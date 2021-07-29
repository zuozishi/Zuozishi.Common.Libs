using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class ProcessMessageResponse : ErrorResponse
    {
        [JsonProperty("data")]
        public ProcessMessageResponseData Data { get; set; }
    }

    public class ProcessMessageResponseData
    {
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
