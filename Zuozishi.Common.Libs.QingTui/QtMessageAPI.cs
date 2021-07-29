using Flurl;
using Flurl.Http;
using HeyRed.Mime;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Zuozishi.Common.Libs.QingTui.Request;
using Zuozishi.Common.Libs.QingTui.Response;

namespace Zuozishi.Common.Libs.QingTui
{
    public class QtMessageAPI
    {
        private readonly QingTuiAPI _qtAPI;

        public QtMessageAPI(QingTuiAPI qtAPI)
        {
            _qtAPI = qtAPI;
        }

        #region 上传多媒体文件
        /// <summary>
        /// 媒体文件类型
        /// </summary>
        /// <param name="stream">二进制流</param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public async Task<MediaUploadResponse> MediaUploadAsync(Stream stream, string fileName, UploadFileType fileType)
        {
            await _qtAPI.CheckAndUpdateToken();
            var resp = await QingTuiAPI.QtHost.AppendPathSegment("v1/media/upload")
                .SetQueryParam("access_token", _qtAPI.AppInfo.AccessToken)
                .SetQueryParam("type", fileType.ToString().ToLower())
                .PostMultipartAsync(mp =>
                {
                    mp.AddFile("media", stream, fileName, MimeTypesMap.GetMimeType(fileName));
                });
            return await resp.GetJsonAsync<MediaUploadResponse>();
        }
        #endregion

        #region 获取使用者
        /// <summary>
        /// 获取使用者列表
        /// </summary>
        /// <param name="page">请求的页码，从1开始</param>
        /// <param name="pageSize">请求页面数量</param>
        /// <returns></returns>
        public async Task<FollowersResponse> GetFollowersAsync(int page, int pageSize)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/app/followers")
                .SetQueryParam("page_size", pageSize)
                .SetQueryParam("request_page", page);
            return await _qtAPI.GetJsonAsync<FollowersResponse>(url);
        }

        /// <summary>
        /// 通过userid获取openid
        /// </summary>
        /// <param name="userId">企业内用户id</param>
        /// <returns></returns>
        public async Task<OpenIdResponse> GetOpenIdByUserId(string userId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/openid/get")
                .SetQueryParam("user_id", userId);
            return await _qtAPI.GetJsonAsync<OpenIdResponse>(url);
        }
        #endregion

        #region 文字消息
        /// <summary>
        /// 群发文字消息
        /// </summary>
        /// <param name="message">发送的文本消息内容</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendTextMessage(string message)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/text/send/service");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                message = new
                {
                    content = message
                }
            });
        }

        /// <summary>
        /// 给部分人发文字消息
        /// </summary>
        /// <param name="message">发送的文本消息内容</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendTextMessage(string message, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/text/send/mass");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = new
                {
                    content = message
                }
            });
        }

        /// <summary>
        /// 发文字消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="message">发送的文本消息内容</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupTextMessage(string channelId, string message)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/text/send/channel");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = new
                {
                    content = message
                }
            });
        }
        #endregion

        #region 图片消息
        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="mediaId">图片id，通过上传多媒体文件方法获得</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendImage(string mediaId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/image/send/service");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                message = new
                {
                    media_id = mediaId
                }
            });
        }

        /// <summary>
        /// 给部分人发图片消息
        /// </summary>
        /// <param name="mediaId">图片id，通过上传多媒体文件方法获得</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendImage(string mediaId, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/image/send/mass");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = new
                {
                    media_id = mediaId
                }
            });
        }

        /// <summary>
        /// 发图片消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="mediaId">图片id，通过上传多媒体文件方法获得</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupImage(string channelId, string mediaId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/image/send/channel");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = new
                {
                    media_id = mediaId
                }
            });
        }
        #endregion

        #region 文本卡片消息
        /// <summary>
        /// 给部分人发文本卡片消息
        /// </summary>
        /// <param name="card">卡片信息</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendTextCardMessage(TextCardMessage card, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/textCard/send/mass");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = card.ToMessageObject()
            });
        }

        /// <summary>
        /// 发文本卡片消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="card">卡片信息</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupTextCardMessage(string channelId, TextCardMessage card)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/textCard/send/channel");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = card.ToMessageObject()
            });
        }
        #endregion

        #region 图文消息
        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="news">图文卡片</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendNews(params NewsMessage[] news)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/news/send/service");
            var article_list = new List<object>();
            foreach (var item in news)
            {
                article_list.Add(item.ToMessageObject());
            }
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                message = new
                {
                    article_list = article_list
                }
            });
        }

        /// <summary>
        /// 给部分人发图文消息
        /// </summary>
        /// <param name="news">图文卡片</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendNews(NewsMessage[] news, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/news/send/mass");
            var article_list = new List<object>();
            foreach (var item in news)
            {
                article_list.Add(item.ToMessageObject());
            }
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = new
                {
                    article_list = article_list
                }
            });
        }

        /// <summary>
        /// 发图文消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="news">图文卡片</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupNews(string channelId, params NewsMessage[] news)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/news/send/channel");
            var article_list = new List<object>();
            foreach (var item in news)
            {
                article_list.Add(item.ToMessageObject());
            }
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = new
                {
                    article_list = article_list
                }
            });
        }
        #endregion

        #region key-value消息
        /// <summary>
        /// 给部分人发key-value消息
        /// </summary>
        /// <param name="keyValue">key-value消息</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendKeyValueMessage(KeyValueMessage keyValue, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/keyValue/send/mass");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = keyValue.ToMessageObject()
            });
        }

        /// <summary>
        /// 发key-value消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="keyValue">key-value消息</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupKeyValueMessage(string channelId, KeyValueMessage keyValue)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/keyValue/send/channel");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = keyValue.ToMessageObject()
            });
        }
        #endregion

        #region 文件消息
        /// <summary>
        /// 群发文件消息
        /// </summary>
        /// <param name="mediaId">文件id，通过上传多媒体文件方法获得</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendFile(string mediaId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/file/send/service");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                message = new
                {
                    media_id = mediaId
                }
            });
        }

        /// <summary>
        /// 给部分人发文件消息
        /// </summary>
        /// <param name="mediaId">文件id，通过上传多媒体文件方法获得</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendFile(string mediaId, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/file/send/mass");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = new
                {
                    media_id = mediaId
                }
            });
        }

        /// <summary>
        /// 发文件消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="mediaId">文件id，通过上传多媒体文件方法获得</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupFile(string channelId, string mediaId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/file/send/channel");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = new
                {
                    media_id = mediaId
                }
            });
        }
        #endregion

        #region 卡片消息
        /// <summary>
        /// 群发文字消息
        /// </summary>
        /// <param name="json">发送的卡片消息内容</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendCardMessage(string json)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/card/send/service");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                message = new
                {
                    content = json
                }
            });
        }

        /// <summary>
        /// 给部分人发文字消息
        /// </summary>
        /// <param name="json">发送的卡片消息内容</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendCardMessage(string json, params string[] users)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/card/send/mass");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                to_users = users,
                message = new
                {
                    content = json
                }
            });
        }

        /// <summary>
        /// 发文字消息至群聊
        /// </summary>
        /// <param name="channelId">要发送的群聊id</param>
        /// <param name="json">发送的卡片消息内容</param>
        /// <returns></returns>
        public async Task<SendMessageResponse> SendGroupCardMessage(string channelId, string json)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/card/send/channel");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                channel_id = channelId,
                message = new
                {
                    content = json
                }
            });
        }
        #endregion

        #region 待办消息
        /// <summary>
        /// 给部分人发待办消息
        /// </summary>
        /// <param name="message">待办信息</param>
        /// <param name="users">要发送的用户openid列表</param>
        /// <returns></returns>
        public async Task<ProcessMessageResponse> SendProcessMessage(ProcessMessage message, string openId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/process/send/single");
            return await _qtAPI.PostJsonAsync<ProcessMessageResponse>(url, new
            {
                to_user = openId,
                message = message.ToMessageObject()
            });
        }

        /// <summary>
        /// 待办消息置为已处理
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<ErrorResponse> CompleteProcessMessage(string msgId, string openId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("v1/message/process/complete");
            return await _qtAPI.PostJsonAsync<SendMessageResponse>(url, new
            {
                msg_id = msgId,
                open_id = openId
            });
        }
        #endregion
    }
}
