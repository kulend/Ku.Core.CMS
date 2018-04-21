//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberPointDto.cs
// 功能描述：会员积分 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 15:52
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.CMS.Domain.Enum.Membership;
using Vino.Core.Infrastructure.Attributes;

namespace Vino.Core.CMS.Domain.Dto.Membership
{
    public class MemberPointDto : BaseDto
    {
        /// <summary>
        /// 积分类型
        /// </summary>
        public EmMemberPointType Type { set; get; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public MemberDto Member { get; set; }

        /// <summary>
	    /// 积分
	    /// </summary>
		public int Points { get; set; }

        /// <summary>
        /// 可用积分
        /// </summary>
        public int UsablePoints { get; set; }

        /// <summary>
        /// 过期积分
        /// </summary>
        public int ExpiredPoints { get; set; }

        /// <summary>
        /// 已用积分
        /// </summary>
        public int UsedPoints { get; set; }
    }
}
