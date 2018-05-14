//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ISmsTempletService.cs
// 功能描述：短信模板 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-02 09:50
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface ISmsTempletService : IBaseService<SmsTemplet, SmsTempletDto, SmsTempletSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(SmsTempletDto dto);
    }
}
