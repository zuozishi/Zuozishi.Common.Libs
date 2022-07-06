using Flurl;
using Flurl.Http;
using Zuozishi.Common.Libs.QingTui.Response;

namespace Zuozishi.Common.Libs.QingTui
{
    public class QingTuiAPI
    {
        public const string QtHost = "https://open.qingtui.cn";

        public readonly QtAppInfo AppInfo;

        public QingTuiAPI(string appId, string appSecret)
        {
            AppInfo = new QtAppInfo
            {
                AppId = appId,
                AppSecret = appSecret
            };
            Message = new QtMessageAPI(this);
            Member = new QtMemberAPI(this);
            Org = new QtOrgAPI(this);
            Web = new QtWebAPI(this);
        }

        public QtMessageAPI Message { get; private set; }

        public QtMemberAPI Member { get; private set; }

        public QtOrgAPI Org { get; private set; }

        public QtWebAPI Web { get; private set; }

        internal async Task<T> GetJsonAsync<T>(Url url) where T : ErrorResponse
        {
            await CheckAndUpdateToken();
            return await url.SetQueryParam("access_token", AppInfo.AccessToken).GetJsonAsync<T>();
        }

        internal async Task<T> PostJsonAsync<T>(Url url, object data) where T : ErrorResponse
        {
            await CheckAndUpdateToken();
            var resp = await url.SetQueryParam("access_token", AppInfo.AccessToken).PostJsonAsync(data);
            return await resp.GetJsonAsync<T>();
        }

        internal async Task<T> PostUrlEncodedAsync<T>(Url url, object data) where T : ErrorResponse
        {
            await CheckAndUpdateToken();
            var resp = await url.SetQueryParam("access_token", AppInfo.AccessToken).PostUrlEncodedAsync(data);
            return await resp.GetJsonAsync<T>();
        }

        public async Task CheckAndUpdateToken()
        {
            if (AppInfo.AccessTokenExpiresTime < DateTime.Now && AppInfo.RefreshTokenExpiresTime > DateTime.Now)
                await RefreshToken();
            if (AppInfo.RefreshTokenExpiresTime < DateTime.Now)
                await UpdateToken();
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        private async Task UpdateToken()
        {
            var resp = await QtHost.AppendPathSegment("v1/token")
                .SetQueryParam("appid", AppInfo.AppId)
                .SetQueryParam("secret", AppInfo.AppSecret)
                .SetQueryParam("grant_type", "client_credential").GetJsonAsync<TokenResponse>();
            AppInfo.AccessToken = resp.AccessToken;
            AppInfo.RefreshToken = resp.RefreshToken;
            AppInfo.AccessTokenExpiresTime = DateTime.Now + TimeSpan.FromSeconds(resp.ExpiresIn);
            AppInfo.RefreshTokenExpiresTime = DateTime.Now + TimeSpan.FromDays(30);
        }

        /// <summary>
        /// 刷新access_token及缓存
        /// </summary>
        /// <returns></returns>
        private async Task RefreshToken()
        {
            var resp = await QtHost.AppendPathSegment("auth/autoRefreshToken")
                .SetQueryParam("refresh_token", AppInfo.RefreshToken).GetJsonAsync<TokenResponse>();
            AppInfo.AccessToken = resp.AccessToken;
            AppInfo.AccessTokenExpiresTime = DateTime.Now + TimeSpan.FromSeconds(resp.ExpiresIn);
        }
    }
}
