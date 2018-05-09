using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.WeChat.User
{
    public class WcUserTagCreateRsp
    {
        [JsonProperty("tag")]
        public WcUserTagItem Tag { set; get; }
    }
}
