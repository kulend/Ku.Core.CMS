using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.WeChat.AccessToken;

namespace Vino.Core.WeChat.Qrcode
{
    public interface IWcQrcodeTool
    {
        /// <summary>
        /// 创建临时二维码
        /// </summary>
        Task<WcReply<WcQrcode>> CreateTemp(WcAccessToken token, int sceneId, int seconds);

        /// <summary>
        /// 创建临时二维码
        /// </summary>
        Task<WcReply<WcQrcode>> Create(WcAccessToken token, int sceneId);
    }
}
