using Newtonsoft.Json;
using System;

namespace Vino.Core.WeChat.AccessToken
{
    public class WcAccessToken
    {
        [JsonProperty("access_token")]
        public string Token { set; get; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { set; get; }

        public DateTime Time { set; get; }
    }
}
