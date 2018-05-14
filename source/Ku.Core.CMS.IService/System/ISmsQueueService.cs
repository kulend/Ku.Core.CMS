//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ISmsQueueService.cs
// 功能描述：短信队列 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface ISmsQueueService : IBaseService<SmsQueue, SmsQueueDto, SmsQueueSearch>
    {

    }
}
