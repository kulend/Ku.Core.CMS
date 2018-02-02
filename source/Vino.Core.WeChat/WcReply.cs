using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.WeChat
{
    public class WcReply<T>
    {
        [JsonProperty("errcode")]
        public int ErrCode { set; get; }

        [JsonProperty("errmsg")]
        public string ErrMsg { set; get; }

        public T Data { set; get; }

        public override string ToString()
        {
            if (ErrCode == 0)
            {
                return "微信接口正常:OK(" + ErrCode + ")";
            }
            else
            {
                return "微信接口出错:" + ErrMsg + "(" + ErrCode + ")";
            }
        }
    }
}
