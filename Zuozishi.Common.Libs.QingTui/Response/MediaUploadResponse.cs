using Newtonsoft.Json;
using Zuozishi.Common.Libs.QingTui.Request;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class MediaUploadResponse : ErrorResponse
    {
        /// <summary>
        /// 媒体文件类型，支持图片（image）和文件（file）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        public UploadFileType FileType
        {
            get
            {
                object type = null;
                System.Enum.TryParse(typeof(UploadFileType), Type, true, out type);
                return (UploadFileType)type;
            }
        }
    }
}
