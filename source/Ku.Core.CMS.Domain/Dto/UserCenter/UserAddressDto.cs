//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserAddressDto.cs
// 功能描述：收货地址 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-01 13:14
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 收货地址
    /// </summary>
    public class UserAddressDto : BaseProtectedDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataType("hidden")]
        public long UserId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [Required, StringLength(20)]
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required]
        [Display(Name = "联系电话")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        /// <summary>
        /// 省份编码
        /// </summary>
        [StringLength(20)]
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        [StringLength(40)]
        [Display(Name = "省份")]
        public string Province { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        [StringLength(20)]
        public string CityCode { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        [StringLength(40)]
        [Display(Name = "城市")]
        public string City { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        [StringLength(20)]
        public string RegionCode { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        [StringLength(40)]
        [Display(Name = "地区")]
        public string Region { get; set; }

        /// <summary>
        /// 街道编码
        /// </summary>
        [StringLength(20)]
        public string StreetCode { get; set; }

        /// <summary>
        /// 街道名称
        /// </summary>
        [StringLength(40)]
        [Display(Name = "街道")]
        public string Street { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(256)]
        [Display(Name = "详细地址")]
        public string Address { get; set; }

        /// <summary>
        /// 默认地址
        /// </summary>
        [Display(Name = "默认地址", Prompt = "是|否")]
        public bool Default { get; set; } = false;

        /// <summary>
        /// 完整地址
        /// </summary>
        [Display(Name = "地址")]
        public string ShowAddress => this.Province + this.City + this.Region + this.Street + this.Address;
    }
}
