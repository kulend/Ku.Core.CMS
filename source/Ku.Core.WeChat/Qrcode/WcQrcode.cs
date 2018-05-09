using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.WeChat.Qrcode
{
    public class WcQrcode
    {
        [JsonProperty("ticket")]
        public string Ticket { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("expire_seconds")]
        public string ExpireSeconds { set; get; }
    }
}
