namespace Zuozishi.Common.Libs.QingTui
{
    public class QtAppInfo
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiresTime { get; set; }
        public DateTime RefreshTokenExpiresTime { get; set; }
    }
}
