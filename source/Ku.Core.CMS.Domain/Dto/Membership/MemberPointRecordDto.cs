//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MemberPointRecordDto.cs
// 功能描述：会员积分记录 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 16:15
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.Membership;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Dto.Membership
{
    /// <summary>
    /// 会员积分记录
    /// </summary>
    public class MemberPointRecordDto : BaseProtectedDto
    {
        /// <summary>
        /// 会员积分ID
        /// </summary>
        public long MemberPointId { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public MemberPointDto MemberPoint { set; get; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public MemberDto Member { set; get; }

        /// <summary>
        /// 变动类型
        /// </summary>
        public EmMemberPointChangeType ChangeType { get; set; }

        /// <summary>
        /// 变动数值
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public EmMemberPointBusType BusType { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public long BusId { get; set; }

        /// <summary>
        /// 业务说明
        /// </summary>
        [StringLength(500)]
        public string BusDesc { get; set; }

        /// <summary>
	    /// 操作员ID
	    /// </summary>
		public long? OperatorId { get; set; }
    }
}
