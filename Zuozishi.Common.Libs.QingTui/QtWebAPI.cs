using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Zuozishi.Common.Libs.QingTui.Response;

namespace Zuozishi.Common.Libs.QingTui
{
    public class QtWebAPI
    {
        private readonly QingTuiAPI _qtAPI;

        public QtWebAPI(QingTuiAPI qtAPI)
        {
            _qtAPI = qtAPI;
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(string qtCode)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/oauth2/userinfo")
                .SetQueryParam("appid", _qtAPI.AppInfo.AppId)
                .SetQueryParam("grant_type", "client_credential")
                .SetQueryParam("qt_code", qtCode)
                .SetQueryParam("secret", _qtAPI.AppInfo.AppSecret);
            Console.WriteLine(url.ToString());
            string json = await url.GetStringAsync();
            return JsonConvert.DeserializeObject<UserInfoResponse>(json);
        }

        public async Task<MemberResponse> GetMemberDetailAsync(string openId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/openid/detail")
                .SetQueryParam("open_id", openId);
            return await _qtAPI.GetJsonAsync<MemberResponse>(url);
        }
    }
}