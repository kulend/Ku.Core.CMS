using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.WeChat.AccessToken;

namespace Vino.Core.WeChat.Qrcode
{
    public class WcQrcodeTool: IWcQrcodeTool
    {
        private readonly ILogger<WcQrcodeTool> _logger;

        private const string URL_QRCODE_CREATE = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";

        public WcQrcodeTool(ILogger<WcQrcodeTool> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 创建临时二维码
        /// </summary>
        public async Task<WcReply<WcQrcode>> CreateTemp(WcAccessToken token, int sceneId, int seconds)
        {
            if (seconds > 2592000) seconds = 2592000;
            if (seconds <= 0) seconds = 30;

            string url = string.Format(URL_QRCODE_CREATE, token.Token);
            var obj = new {
                expire_seconds = seconds,
                action_name = "QR_SCENE",
                action_info = new {
                    scene = new {
                        scene_id = sceneId
                    }
                }
            };
            var res = await HttpHelper.HttpPostAsync(url, JsonConvert.SerializeObject(obj));
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcQrcode> error = JsonConvert.DeserializeObject<WcReply<WcQrcode>>(res);
                _logger.LogError(error.ToString());
                return error;
            }
            WcQrcode data = JsonConvert.DeserializeObject<WcQrcode>(res);
            return new WcReply<WcQrcode>
            {
                Data = data,
                ErrCode = 0
            };
        }

        /// <summary>
        /// 创建临时二维码
        /// </summary>
        public async Task<WcReply<WcQrcode>> Create(WcAccessToken token, int sceneId)
        {
            string url = string.Format(URL_QRCODE_CREATE, token.Token);
            var obj = new
            {
                action_name = "QR_LIMIT_SCENE",
                action_info = new
                {
                    scene = new
                    {
                        scene_id = sceneId
                    }
                }
            };
            var res = await HttpHelper.HttpPostAsync(url, JsonConvert.SerializeObject(obj));
            if (res.IndexOf("errcode") >= 0)
            {
                WcReply<WcQrcode> error = JsonConvert.DeserializeObject<WcReply<WcQrcode>>(res);
                _logger.LogError(error.ToString());
                return error;
            }
            WcQrcode data = JsonConvert.DeserializeObject<WcQrcode>(res);
            return new WcReply<WcQrcode>
            {
                Data = data,
                ErrCode = 0
            };
        }
    }
}
