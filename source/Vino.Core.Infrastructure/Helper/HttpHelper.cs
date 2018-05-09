using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ku.Core.Infrastructure.Helper
{
    public static class HttpHelper
    {
        /// <summary>
        /// post请求
        /// </summary>
        public static async Task<string> HttpPostAsync(string url, string data)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = null;
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpClient http = new HttpClient(handler))
            {
                var content = new StringContent(data);
                //await异步等待回应
                var response = await http.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// get请求
        /// </summary>
        public static async Task<string> HttpGetAsync(string url, string data = null)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                if (data.StartsWith("?") || data.StartsWith("&"))
                {
                    data = data.Substring(1);
                }
                if (url.IndexOf("?") > 0)
                {
                    url += $"&{data}";
                }
                else
                {
                    url += $"?{data}";
                }
            }

            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = null;
            handler.AutomaticDecompression = DecompressionMethods.GZip;

            using (var http = new HttpClient(handler))
            {
                var response = await http.GetAsync(url);
                //确保HTTP成功状态值
                response.EnsureSuccessStatusCode();
                //string responseStr = response.Content.ReadAsStringAsync().Result;
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
