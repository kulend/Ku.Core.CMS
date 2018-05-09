using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.WeChat.User
{
    public class WcUserTagListRsp
    {
        [JsonProperty("tags")]
        public List<WcUserTagItem> Tags { set; get; }
    }

    public class WcUserTagItem
    {
        [JsonProperty("id")]
        public int Id { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("count")]
        public int Count { set; get; }
    }
}
