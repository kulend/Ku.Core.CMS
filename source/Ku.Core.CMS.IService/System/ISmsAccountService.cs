//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ISmsAccountService.cs
// 功能描述：短信账号 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-03-26 16:05
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface ISmsAccountService : IBaseService<SmsAccount, SmsAccountDto, SmsAccountSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(SmsAccountDto dto);
    }
}
