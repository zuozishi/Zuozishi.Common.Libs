namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class ErrorResponse
    {
        /// <summary>
        /// 错误代号
        /// </summary>
        public int? ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
