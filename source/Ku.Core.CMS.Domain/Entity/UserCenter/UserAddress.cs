//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserAddress.cs
// 功能描述：收货地址 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-01 13:14
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 收货地址
    /// </summary>
    [Table("usercenter_user_address")]
    public class UserAddress : BaseProtectedEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Required, StringLength(20)]
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required, StringLength(20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        [StringLength(20)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 省份编码
        /// </summary>
        [StringLength(20)]
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        [StringLength(40)]
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
        public string Street { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(256)]
        public string Address { get; set; }

        /// <summary>
        /// 默认地址
        /// </summary>
        public bool Default { get; set; } = false;
    }

    /// <summary>
    /// 收货地址 检索条件
    /// </summary>
    public class UserAddressSearch : BaseProtectedSearch<UserAddress>
    {

    }
}
