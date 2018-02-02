using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.WeChat.User
{
    public class WcUserDetailRsp
    {
        [JsonProperty("subscribe")]
        public int Subscribe { set; get; }

        [JsonProperty("openid")]
        public string Openid { set; get; }

        [JsonProperty("nickname")]
        public string Nickname { set; get; }

        [JsonProperty("sex")]
        public int Sex { set; get; }

        [JsonProperty("language")]
        public string Language { set; get; }

        [JsonProperty("city")]
        public string City { set; get; }

        [JsonProperty("province")]
        public string Province { set; get; }

        [JsonProperty("country")]
        public string Country { set; get; }

        [JsonProperty("headimgurl")]
        public string Headimgurl { set; get; }

        [JsonProperty("subscribe_time")]
        public long SubscribeTime { set; get; }

        [JsonProperty("unionid")]
        public string Unionid { set; get; }

        [JsonProperty("remark")]
        public string Remark { set; get; }

        [JsonProperty("tagid_list")]
        public int[] TagidList { set; get; }
    }
}
