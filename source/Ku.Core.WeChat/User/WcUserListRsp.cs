using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.WeChat.User
{
    public class WcUserListRsp
    {
        [JsonProperty("total")]
        public int Total { set; get; }

        [JsonProperty("count")]
        public int Count { set; get; }

        [JsonProperty("data")]
        public WcUserListData Data { set; get; }
    }

    public class WcUserListData
    {
        [JsonProperty("next_openid")]
        public string NextOpenid { set; get; }

        [JsonProperty("openid")]
        public string[] Openid { set; get; }
    }
}
