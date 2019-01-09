using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Domain.Base
{
    public class JsonUploadImage
    {
        /// <summary>
        /// 排序值
        /// </summary>
        [JsonProperty("index")]
        public int Index { set; get; }

        /// <summary>
        /// 大图路径
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        /// <summary>
        /// 大图地址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }


        /// <summary>
        /// 缩略图地址
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        public static List<JsonUploadImage> Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new List<JsonUploadImage>();
            }
            try
            {
                return JsonConvert.DeserializeObject<List<JsonUploadImage>>(data);
            }
            catch
            {
                return new List<JsonUploadImage>();
            }
        }
    }
}
