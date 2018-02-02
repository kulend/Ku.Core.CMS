using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.WeChat.AccessToken
{
    public interface IWcAccessTokenTool
    {
        /// <summary>
        /// 取得微信AccessToken
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns>WxAccessToken</returns>
        Task<WcReply<WcAccessToken>> GetAsync(string appId, string appSecret);

        /// <summary>
        /// 刷新微信AccessToken
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns>WxAccessToken</returns>
        Task<WcReply<WcAccessToken>> RefreshAsync(string appId, string appSecret);
    }
}
