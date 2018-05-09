using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ku.Core.Cache;
using Ku.Core.Infrastructure.Helper;

namespace Ku.Core.WeChat.AccessToken
{
    public class WcAccessTokenTool : IWcAccessTokenTool
    {
        private readonly ILogger<WcAccessTokenTool> _logger;

        private const string CACHE_ACCESSTOKEN = "vino.cache.wechat.accesstoken:{0}";
        private const string URL_GET_TOKEN = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        private ICacheService cacheService;

        public WcAccessTokenTool(ICacheService cache, ILogger<WcAccessTokenTool> logger)
        {
            this.cacheService = cache;
            this._logger = logger;
        }

        /// <summary>
        /// 取得微信AccessToken
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns>WxAccessToken</returns>
        public async Task<WcReply<WcAccessToken>> GetAsync(string appId, string appSecret)
        {
            //先判断缓存是否有数据
            var token = cacheService.Get<WcAccessToken>(string.Format(CACHE_ACCESSTOKEN, appId));

            //判断是否超时
            if (token != null && token.Time < DateTime.Now)
            {
                token = null;
            }

            if (token == null)
            {
                //取得Token
                WcReply<WcAccessToken> data = await RefreshAsync(appId, appSecret);
                return data;
            }

            return new WcReply<WcAccessToken>
            {
                Data = token,
                ErrCode = 0
            };
        }

        /// <summary>
        /// 刷新微信AccessToken
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns>WxAccessToken</returns>
        public async Task<WcReply<WcAccessToken>> RefreshAsync(string appId, string appSecret)
        {
            var res = await HttpHelper.HttpGetAsync(string.Format(URL_GET_TOKEN, appId, appSecret));
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcAccessToken> error = JsonConvert.DeserializeObject<WcReply<WcAccessToken>>(res);
                _logger.LogError(error.ToString());
                return error;
            }

            WcAccessToken token = JsonConvert.DeserializeObject<WcAccessToken>(res);
            token.Time = DateTime.Now.AddSeconds(token.ExpiresIn - 600); //600秒做缓冲时间

            //保存到缓存
            cacheService.Add(string.Format(CACHE_ACCESSTOKEN, appId), token, new TimeSpan((token.ExpiresIn - 600) * (long)10_000_000));

            return new WcReply<WcAccessToken>
            {
                Data = token,
                ErrCode = 0
            };
        }

    }
}
