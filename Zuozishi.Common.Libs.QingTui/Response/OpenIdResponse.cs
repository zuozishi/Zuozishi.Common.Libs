using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class OpenIdResponse : ErrorResponse
    {
        /// <summary>
        /// 用户使用轻应用生成的唯一id
        /// </summary>
        [JsonProperty("open_id")]
        public string OpenId { get; set; }
    }
}
