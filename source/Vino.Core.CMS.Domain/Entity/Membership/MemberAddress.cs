//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberAddress.cs
// 功能描述：会员地址 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-02 14:29
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Membership
{
    /// <summary>
    /// 会员地址
    /// </summary>
    [Table("membership_member_address")]
    public class MemberAddress : BaseProtectedEntity
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual Member Member { get; set; }

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
    /// 会员地址 检索条件
    /// </summary>
    public class MemberAddressSearch : BaseProtectedSearch<MemberAddress>
    {

    }
}
