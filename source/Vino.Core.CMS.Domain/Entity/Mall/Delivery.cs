//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：Delivery.cs
// 功能描述：物流配送 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:05
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Mall
{
    [Table("Mall_Delivery")]
    public class Delivery : BaseProtectedEntity
    {

    }

    public class DeliverySearch : BaseSearch<Delivery>
    {

    }
}
