using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Ku.Core.CMS.Web.Base
{
    public class PagerResult<T>
    {
        [JsonProperty("pager")]
        public Pager Pager { set; get; }

        [JsonProperty("items")]
        public IEnumerable<T> Items { set; get; }

        public PagerResult(IEnumerable<T> items, int page, int size, int total)
        {
            this.Items = items;
            this.Pager = new Pager(page, size, total);
        }
    }

    public class Pager
    {
        [JsonProperty("page")]
        public int Page { set; get; }

        [JsonProperty("rows")]
        public int Rows { set; get; }

        [JsonProperty("total_page")]
        public int TotalPage { set; get; }

        [JsonProperty("total")]
        public int Total { set; get; }

        public Pager()
        {
        }

        public Pager(int page, int rows, int total)
        {
            Page = page;
            Rows = rows;
            TotalPage = Convert.ToInt32(Math.Ceiling((total * 1.00) / rows));
            Total = total;
        }
    }

    /// <summary>
    /// 原始Json数据，不会进行其他封装
    /// </summary>
    public class OriginJsonResult : JsonResult
    {
        public OriginJsonResult(object value) : base(value)
        {

        }
    }

    #region Layui

    public class LayuiPagerResult<T>
    {
        [JsonProperty("code")]
        public int Code { set; get; } = 0;

        [JsonProperty("msg")]
        public string Message { set; get; } = "";

        [JsonProperty("count")]
        public int Count { set; get; }

        [JsonProperty("data")]
        public IEnumerable<T> Items { set; get; }

        public LayuiPagerResult(IEnumerable<T> items, int page, int size, int total)
        {
            this.Items = items;
            this.Count = total;
        }
    }

    public class LayuiJsonResult : OriginJsonResult
    {
        public LayuiJsonResult(object value) : base(value)
        {

        }
    }

    #endregion

}
