using Newtonsoft.Json;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class TokenResponse : ErrorResponse
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
        /// <summary>
        /// 刷新凭证，有效期30天，access_token失效后，可通过此凭证获取新的access_token，尽可能使用此刷新凭证获取最新access_token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
