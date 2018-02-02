using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.WeChat.AccessToken;

namespace Vino.Core.WeChat.User
{
    public class WcUserTool: IWcUserTool
    {
        private readonly ILogger<WcUserTool> _logger;

        private const string URL_TAGS_GET = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token={0}";
        private const string URL_TAG_CREATE = "https://api.weixin.qq.com/cgi-bin/tags/create?access_token={0}";
        private const string URL_TAG_UPDATE = "https://api.weixin.qq.com/cgi-bin/tags/update?access_token={0}";
        private const string URL_TAG_DELETE = "https://api.weixin.qq.com/cgi-bin/tags/delete?access_token={0}";

        private const string URL_USERS_LIST = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";
        private const string URL_USERS_DETAIL = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        private ICacheService cacheService;

        public WcUserTool(ICacheService cache, ILogger<WcUserTool> logger)
        {
            this.cacheService = cache;
            this._logger = logger;
        }

        #region 用户标签

        /// <summary>
        /// 取得用户标签列表
        /// </summary>
        public async Task<WcReply<WcUserTagListRsp>> GetUserTagListAsync(WcAccessToken token)
        {
            var res = await HttpHelper.HttpGetAsync(string.Format(URL_TAGS_GET, token.Token));
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcUserTagListRsp> error = JsonConvert.DeserializeObject<WcReply<WcUserTagListRsp>>(res);
                _logger.LogError(error.ToString());
                return error;
            }

            WcUserTagListRsp data = JsonConvert.DeserializeObject<WcUserTagListRsp>(res);
            return new WcReply<WcUserTagListRsp>
            {
                Data = data,
                ErrCode = 0
            };
        }

        /// <summary>
        /// 创建用户标签
        /// </summary>
        public async Task<WcReply<WcUserTagCreateRsp>> CreateUserTag(WcAccessToken token, string name)
        {
            string url = string.Format(URL_TAG_CREATE, token.Token);
            var res = await HttpHelper.HttpPostAsync(url, "{\"tag\":{\"name\":\"" + name + "\"}}");
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcUserTagCreateRsp> error = JsonConvert.DeserializeObject<WcReply<WcUserTagCreateRsp>>(res);
                _logger.LogError(error.ToString());
                return error;
            }

            WcUserTagCreateRsp data = JsonConvert.DeserializeObject<WcUserTagCreateRsp>(res);
            return new WcReply<WcUserTagCreateRsp>
            {
                Data = data,
                ErrCode = 0
            };
        }

        /// <summary>
        /// 更新用户标签
        /// </summary>
        public async Task<WcReply<string>> UpdateUserTag(WcAccessToken token, int tagId, string name)
        {
            string url = string.Format(URL_TAG_UPDATE, token.Token);
            var res = await HttpHelper.HttpPostAsync(url, "{\"tag\":{\"id\":" + tagId + ", \"name\":\"" + name + "\"}}");
            WcReply<string> rsp = JsonConvert.DeserializeObject<WcReply<string>>(res);
            if (rsp.ErrCode != 0)
            {
                _logger.LogError(rsp.ToString());
            }
            return rsp;
        }

        /// <summary>
        /// 删除用户标签
        /// </summary>
        public async Task<WcReply<string>> DeleteUserTag(WcAccessToken token, int tagId)
        {
            string url = string.Format(URL_TAG_DELETE, token.Token);
            var res = await HttpHelper.HttpPostAsync(url, "{\"tag\":{\"id\":" + tagId + "}}");
            WcReply<string> rsp = JsonConvert.DeserializeObject<WcReply<string>>(res);
            if (rsp.ErrCode != 0)
            {
                _logger.LogError(rsp.ToString());
            }
            return rsp;
        }

        #endregion

        #region 用户

        /// <summary>
        /// 取得微信用户列表
        /// </summary>
        public async Task<WcReply<WcUserListRsp>> GetUserListAsync(WcAccessToken token, string nextOpenId)
        {
            var res = await HttpHelper.HttpGetAsync(string.Format(URL_USERS_LIST, token.Token, nextOpenId ?? ""));
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcUserListRsp> error = JsonConvert.DeserializeObject<WcReply<WcUserListRsp>>(res);
                _logger.LogError(error.ToString());
                return error;
            }

            WcUserListRsp data = JsonConvert.DeserializeObject<WcUserListRsp>(res);
            return new WcReply<WcUserListRsp>
            {
                Data = data,
                ErrCode = 0
            };
        }

        /// <summary>
        /// 取得微信用户信息
        /// </summary>
        public async Task<WcReply<WcUserDetailRsp>> GetUserDetailAsync(WcAccessToken token, string openId)
        {
            var res = await HttpHelper.HttpGetAsync(string.Format(URL_USERS_DETAIL, token.Token, openId));
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcUserDetailRsp> error = JsonConvert.DeserializeObject<WcReply<WcUserDetailRsp>>(res);
                _logger.LogError(error.ToString());
                return error;
            }

            WcUserDetailRsp data = JsonConvert.DeserializeObject<WcUserDetailRsp>(res);
            return new WcReply<WcUserDetailRsp>
            {
                Data = data,
                ErrCode = 0
            };
        }
        #endregion
    }
}
